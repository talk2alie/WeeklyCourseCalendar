using System.IO;

namespace WeeklyCourseCalendar.Domain.Services
{
    public class WeeklyScheduleWriter : IWeeklyScheduleWriter
    {
        public void WriteAsHtml(WeeklySchedule weeklySchedule, string outputPath)
        {
            string weeklyScheduleHtmlPage = GetDefaultHtmlPageTemplate();
            weeklyScheduleHtmlPage = weeklyScheduleHtmlPage
                .Replace("#SemesterName#", weeklySchedule.SemesterName)
                .Replace("#SemesterStartDate#", weeklySchedule.SemesterStartDate.ToString("MMMM dd, yyyy"))
                .Replace("#SemesterEndDate#", weeklySchedule.SemesterEndDate.ToString("MMMM dd, yyyy"));
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            using (var stream = new FileStream(outputPath, FileMode.CreateNew, FileAccess.Write))
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.Write(weeklyScheduleHtmlPage);
            }
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
                    </head>
                    <body>
                        <div class='container-fluid'>
                            <h1>
                                <span>#SemesterName#</span>
                                <br>
                                <small>#SemesterStartDate# - #SemesterEndDate#</small>
                            </h1>
                            <table class='table table-striped'>
                                <thead>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                        <script src='https://ajax.aspnetcdn.com/ajax/bootstrap/4.0.0/bootstrap.min.js'></script>
                    </body>
                    </html>";
        }
    }
}