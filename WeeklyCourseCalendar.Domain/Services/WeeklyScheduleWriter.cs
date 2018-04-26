using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeeklyCourseCalendar.Domain.Helpers;

namespace WeeklyCourseCalendar.Domain.Services
{
    public class WeeklyScheduleWriter : IWeeklyScheduleWriter
    {
        public string WriteAsHtml(WeeklySchedule weeklySchedule, string outputPath)
        {
            string weeklyScheduleHtmlPage = GetDefaultHtmlPageTemplate();
            weeklyScheduleHtmlPage = SetPageHeader(weeklySchedule, weeklyScheduleHtmlPage);
            weeklyScheduleHtmlPage = SetHtmlTableHeaderRow(weeklySchedule, weeklyScheduleHtmlPage);
            weeklyScheduleHtmlPage = SetHtmlTableBodyRows(weeklySchedule, weeklyScheduleHtmlPage);

            outputPath = EnsureFileCanBeCreated(outputPath);
            using (var stream = new FileStream(outputPath, FileMode.CreateNew, FileAccess.Write))
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.Write(weeklyScheduleHtmlPage);
            }
            return outputPath;
        }

        private string GetDefaultHtmlPageTemplate()
        {
            return @"<!doctype <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset='utf-8' />
                        <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                        <title>#SemesterName# - Weekly Class Schedule</title>
                        <meta name='viewport' content='width=device-width, initial-scale=1'>
                        <link rel='stylesheet'
                              type='text/css' media='screen'
                              href='https://ajax.aspnetcdn.com/ajax/bootstrap/4.0.0/css/bootstrap.min.css'
                        />
                        <style>
                            th, td
                            {
                                vertical-align: middle !important;
                                text-align: center; !important;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container-fluid'>
                            <h1>
                                <span>#SemesterName#</span>
                                <br>
                                <small>#SemesterStartDate# - #SemesterEndDate#</small>
                            </h1>
                            <table class='table table-bordered'>
                                <thead>#TableHeader#</thead>
                                <tbody>#TableBody#</tbody>
                            </table>
                        </div>
                        <script src='https://ajax.aspnetcdn.com/ajax/bootstrap/4.0.0/bootstrap.min.js'></script>
                    </body>
                    </html>";
        }

        private string SetPageHeader(WeeklySchedule weeklySchedule, string weeklyScheduleHtmlPage)
        {
            weeklyScheduleHtmlPage = weeklyScheduleHtmlPage
                            .Replace("#SemesterName#", weeklySchedule.SemesterName)
                            .Replace("#SemesterStartDate#", weeklySchedule.SemesterStartDate.ToString("MMMM dd, yyyy"))
                            .Replace("#SemesterEndDate#", weeklySchedule.SemesterEndDate.ToString("MMMM dd, yyyy"));
            return weeklyScheduleHtmlPage;
        }

        private string SetHtmlTableHeaderRow(WeeklySchedule weeklySchedule, string weeklyScheduleHtmlPage)
        {
            string theadContent = $"<th>Date</th>";
            foreach (DateTime time in weeklySchedule.SchoolTimes)
            {
                theadContent += $"<th>{time.ToShortTimeString()}</th>";
            };
            weeklyScheduleHtmlPage = weeklyScheduleHtmlPage.Replace("#TableHeader#", theadContent);
            return weeklyScheduleHtmlPage;
        }

        private string EnsureFileCanBeCreated(string fileName)
        {
            fileName = Path.ChangeExtension(fileName, ".html");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            return fileName;
        }

        private string SetHtmlTableBodyRows(WeeklySchedule weeklySchedule, string weeklyScheduleHtmlPage)
        {
            string tbodyContent = String.Empty;
            foreach (DayOfWeek day in weeklySchedule.SchoolDays)
            {
                int rowSpan = CalculateRowsDaySpans(day, weeklySchedule);
                if (rowSpan == 0)
                {
                    continue;
                }

                var dayColumns = new Dictionary<string, string>();

                int rowIndex = 0;
                var dayRows = new Dictionary<string, string>();
                while (rowIndex < rowSpan)
                {
                    string row = String.Empty;
                    if (rowIndex == 0)
                    {
                        row += $"<tr><td rowspan='{rowSpan}'>{day.ToString()}</td>";
                    }
                    else
                    {
                        row = "<tr>";
                    }
                    dayRows.Add(GetRowIdFromIndex(rowIndex), row);

                    // Populate all columns for a given row within a day
                    foreach (DateTime time in weeklySchedule.SchoolTimes)
                    {
                        dayColumns.Add(GetColumnId(day, time, rowIndex), $"<td>&nbsp;</td>");
                    }
                    rowIndex++;
                }

                foreach (DateTime time in weeklySchedule.SchoolTimes)
                {
                    string slotId = TimeSlotHelpers.GenerateIdFromDaysAndTime(day, time);
                    TimeSlot timeSlot = weeklySchedule.TimeSlots.SingleOrDefault(slot =>
                        slot.Id.Equals(slotId, StringComparison.InvariantCulture));
                    if (timeSlot == null)
                    {
                        continue;
                    }

                    for (int classIndex = 0; classIndex < timeSlot.Classes.Count(); classIndex++)
                    {
                        Class @class = timeSlot.Classes.ElementAt(classIndex);
                        string columnId = GetColumnId(day, @class.StartTime, classIndex);
                        string td = dayColumns[columnId];

                        td = $@"<td colspan={timeSlot.SlotSpan}>" +
                                      $"{@class.Name} - {@class.Section}<br>" +
                                      $"{@class.Title}<br>" +
                                      $"{@class.StartTime.ToShortTimeString()} to {@class.EndTime.ToShortTimeString()}" +
                                "</td>";
                        dayColumns[columnId] = td;
                        RemoveSpannedOverColumns(@class.StartTime, @class.EndTime, classIndex, day, dayColumns);
                    }
                }

                rowIndex = 0;
                while (rowIndex < rowSpan)
                {
                    string rowData = dayRows[GetRowIdFromIndex(rowIndex)];
                    IEnumerable<string> keys = dayColumns.Keys.Where(key => key.Contains($"_{rowIndex}_"));

                    foreach (string key in keys)
                    {
                        rowData += dayColumns[key];
                    }
                    tbodyContent += rowData + "</tr>";
                    rowIndex++;
                }
            }

            weeklyScheduleHtmlPage = weeklyScheduleHtmlPage.Replace("#TableBody#", tbodyContent);
            return weeklyScheduleHtmlPage;
        }

        private int CalculateRowsDaySpans(DayOfWeek day, WeeklySchedule weeklySchedule)
        {
            IEnumerable<TimeSlot> timeSlots = weeklySchedule.TimeSlots.Where(timeSlot => timeSlot.Day == day);
            if (timeSlots.Count() == 0)
            {
                return 0;
            }
            return timeSlots.Max(timeSlot => timeSlot.Classes.Count());
        }

        private string GetRowIdFromIndex(int index)
        {
            return $"row{index + 1}";
        }

        private string GetColumnId(DayOfWeek day, DateTime time, int rowIndex)
        {
            return $"{day.ToString()}_{rowIndex}_{time.ToShortTimeString()}".Replace(" ", "");
        }

        private void RemoveSpannedOverColumns(DateTime startTime,
            DateTime endTime, int classIndex, DayOfWeek day, Dictionary<string, string> dayColumns)
        {
            DateTime currentTime = startTime.AddMinutes(5);
            while (currentTime <= endTime)
            {
                string columnId = GetColumnId(day, currentTime, classIndex);
                dayColumns.Remove(columnId);
                currentTime = currentTime.AddMinutes(5);
            }
        }
    }
}