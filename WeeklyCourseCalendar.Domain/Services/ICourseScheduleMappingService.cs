using System;
using System.Collections.Generic;
using System.Text;
using WeeklyCourseCalendar.Domain.Models;

namespace WeeklyCourseCalendar.Services
{
    public interface ICourseScheduleMappingService
    {
        List<Class> ParseCourseScheduleText();
    }
}
