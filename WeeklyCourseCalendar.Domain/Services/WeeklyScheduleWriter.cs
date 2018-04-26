﻿using System;
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

                var dayRows = new Dictionary<string, string>();
                int rowIndex = 0;
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
                        string rowId = GetRowIdFromIndex(classIndex);
                        dayRows.TryGetValue(rowId, out string row);
                        row += $@"<td colspan={timeSlot.SlotSpan}>" +
                                      $"{@class.Name} - {@class.Section}<br>" +
                                      $"{@class.Title}<br>" +
                                      $"{@class.StartTime.ToShortTimeString()} to {@class.EndTime.ToShortTimeString()}" +
                                "</td>";
                        dayRows[rowId] = row;
                    }
                }

                rowIndex = 0;
                while (rowIndex < rowSpan)
                {
                    tbodyContent += dayRows[GetRowIdFromIndex(rowIndex)] + "</tr>";
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
    }
}