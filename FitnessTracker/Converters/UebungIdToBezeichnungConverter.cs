using System;
using System.Globalization;
using System.Linq;
using FitnessTracker.Models;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace FitnessTracker.Converters
{
    public class UebungIdToBezeichnungConverter : IValueConverter
    {
        public IList<Uebung> Uebungen { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int uebungId && Uebungen != null)
            {
                var uebung = Uebungen.FirstOrDefault(u => u.UebungID == uebungId);
                return uebung?.Bezeichnung ?? $"ID: {uebungId}";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}