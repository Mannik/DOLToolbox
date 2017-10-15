using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace MannikToolbox.Services
{
    class BindingService
    {
        public static void BindData<T, TF>(T source, TF form) where TF : UserControl
        {
            var formFields = form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var mobProperty in source.GetType().GetProperties())
            {
                if (formFields.All(x => x.Name != "_" + mobProperty.Name))
                {
                    continue;
                }

                var formProperty = formFields.First(x => x.Name == "_" + mobProperty.Name);

                switch (formProperty.GetValue(form))
                {
                    case TextBox input:
                        input.Text = mobProperty.GetValue(source)?.ToString();
                        break;
                    case CheckBox input:
                        input.Checked = mobProperty.GetValue(source) is bool && (bool)mobProperty.GetValue(source);
                        break;
                    case ComboBox input:
                        if (!int.TryParse(mobProperty.GetValue(source)?.ToString(), out var value))
                        {
                            break;
                        }

                        var item = input.Items.Cast<KeyValuePair<int, string>>().FirstOrDefault(x => x.Key == value);

                        if (item.Key == default(int))
                        {
                            break;
                        }

                        input.SelectedItem = item;
                        break;
                }
            }
        }

        public static void SyncData<T, TF>(T destination, TF form) where TF : UserControl
        {
            var formFields = form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var mobProperty in typeof(T).GetProperties())
            {
                if (formFields.All(x => x.Name != "_" + mobProperty.Name))
                {
                    continue;
                }

                var formProperty = formFields.First(x => x.Name == "_" + mobProperty.Name);

                switch (formProperty.GetValue(form))
                {
                    case TextBox input:
                        var value = !string.IsNullOrWhiteSpace(input.Text) ? input.Text : null;
                        BindValueFromString(value, mobProperty, destination);
                        break;
                    case CheckBox input:
                        mobProperty.SetValue(destination, input.Checked);
                        break;
                    case ComboBox input:
                        if (input.SelectedItem == null)
                        {
                            mobProperty.SetValue(destination, null);                            
                        }

                        var selected = (KeyValuePair<int, string>)input.SelectedItem;
                        BindValueFromInt(selected.Key, mobProperty, destination);
                        break;
                }
            }
        }

        private static void BindValueFromInt<T>(int input, PropertyInfo property, T obj)
        {
            var propertyType = property.PropertyType;

            if (propertyType == typeof(int))
            {
                property.SetValue(obj, input);
            }
            else if (propertyType == typeof(byte))
            {
                property.SetValue(obj, (byte)input);
            }
            else if (propertyType == typeof(uint))
            {
                property.SetValue(obj, (uint)input);
            }
            else if (propertyType == typeof(ushort))
            {
                property.SetValue(obj, (ushort)input);
            }
            else
            {
                throw new ApplicationException($"Failed to bind {property.Name} of type {propertyType.Name}");
            }
        }

        private static void BindValueFromString<T>(string input, PropertyInfo property, T obj)
        {
            var propertyType = property.PropertyType;

            if (propertyType == typeof(string))
            {
                property.SetValue(obj, input);
            }
            else if (propertyType == typeof(byte))
            {
                var success = byte.TryParse(input, out byte parsed);
                if (!success)
                {
                    throw new ApplicationException($"Failed to parse {property.Name} into type byte");
                }

                property.SetValue(obj, parsed);
            }
            else if (propertyType == typeof(int))
            {
                var success = int.TryParse(input, out int parsed);
                if (!success)
                {
                    throw new ApplicationException($"Failed to parse {property.Name} into type int");
                }

                property.SetValue(obj, parsed);
            }
            else if (propertyType == typeof(uint))
            {
                var success = uint.TryParse(input, out uint parsed);
                if (!success)
                {
                    throw new ApplicationException($"Failed to parse {property.Name} into type uint");
                }

                property.SetValue(obj, parsed);
            }
            else if (propertyType == typeof(ushort))
            {
                var success = ushort.TryParse(input, out ushort parsed);
                if (!success)
                {
                    throw new ApplicationException($"Failed to parse {property.Name} into type ushort");
                }

                property.SetValue(obj, parsed);
            }
            else if (propertyType == typeof(DateTime))
            {
                var success = DateTime.TryParse(input, out DateTime parsed);
                if (!success)
                {
                    throw new ApplicationException($"Failed to parse {property.Name} into type ushort");
                }

                property.SetValue(obj, parsed);
            }
            else
            {
                throw new ApplicationException($"Unsupported binding type {propertyType.Name}");
            }
        }
    }
}
