using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchApi.Models;

namespace ChurchApi.Services
{
    public class EnumData
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public interface IEnumService
    {
        List<EnumData> GetList(string name);
    }

    public class EnumService:IEnumService
    {
        public List<EnumData> GetList(string name)
        {
            switch (name)
            {
                case "DayOfWeek":
                    return GetEnumValues<DayOfWeek>();
                case "View":
                    return GetEnumValues<View>();
                default:
                    throw new ArgumentOutOfRangeException("Unknown Enum");

            }

        }

        private static List<EnumData> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(x => new EnumData
            {
                Value = x.ToString(),
                Label = x.ToString().Titleize()
            }).ToList();
        }
    }
}
