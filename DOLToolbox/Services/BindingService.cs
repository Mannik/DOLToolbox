using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DOLToolbox.Services
{
    public static class BindingService
    {
        public static void ToggleEnabled<T>(T form, bool? overrideValue = null)
            where T : UserControl
        {
            typeof(T)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.FieldType.IsSubclassOf(typeof(Control)))
                .ToList()
                .ForEach(x =>
                {
                    if (x.GetValue(form) is Control control)
                    {
                        control.Enabled = overrideValue ?? !control.Enabled;
                    }
                });
        }

        public static void ClearData<T>(T form)
            where T : UserControl
        {
            var formFields = form.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var formField in formFields)
            {
                switch (formField.GetValue(form))
                {
                    case TextBox input:
                        input.Text = null;
                        break;
                    case CheckBox input:
                        input.Checked = false;
                        break;
                    case RadioButton input:
                        input.Checked = false;
                        break;
                    case ComboBox input:
                        if (input.Items.Count > 0)
                        {
                            input.SelectedIndex = 0;
                        }
                        break;
                    case RichTextBox input:
                        input.Text = null;
                        break;
                    case ListBox input:
                        input.SelectedItems.Clear();
                        break;
                }
            }
        }
        public static void BindData<T, TF>(T source, TF form)
            where TF : UserControl
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
                    case RichTextBox input:
                        input.Text = mobProperty.GetValue(source)?.ToString();
                        break;
                    case CheckBox input:
                        input.Checked = mobProperty.GetValue(source) is bool && (bool)mobProperty.GetValue(source);
                        break;
                    case ComboBox input:
                        var sourceValue = mobProperty.GetValue(source)?.ToString();
                        if (int.TryParse(sourceValue, out var value))
                        {
                            var item = input.Items.Cast<ComboboxService.SelectItemModel>().FirstOrDefault(x => x.Id == value);

                            if (item?.Id == null)
                            {
                                break;
                            }

                            input.SelectedItem = item;
                        }
                        else if (mobProperty.PropertyType == typeof(string))
                        {
                            var item = input.Items.Cast<ComboboxService.SelectItemModel>().FirstOrDefault(x => x.Value == sourceValue);

                            if (item?.Id == null)
                            {
                                break;
                            }

                            input.SelectedItem = item;
                        }

                        break;
                }
            }
        }

        public static void SyncData<T, TF>(T destination, TF form)
            where TF : UserControl
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

                        var selected = (ComboboxService.SelectItemModel)input.SelectedItem;
                        BindValueFromCombobox(selected, mobProperty, destination);
                        break;
                }
            }
        }

        private static void BindValueFromCombobox<T>(ComboboxService.SelectItemModel input, PropertyInfo property, T obj)
        {
            if (input?.Id == null)
            {
                property.SetValue(obj, GetDefault(property.PropertyType));
                return;
            }

            var propertyType = property.PropertyType;

            if (propertyType == typeof(string))
            {
                property.SetValue(obj, input.Value);
            }
            else if (propertyType == typeof(int))
            {
                property.SetValue(obj, input.Id);
            }
            else if (propertyType == typeof(byte))
            {
                property.SetValue(obj, (byte)input.Id);
            }
            else if (propertyType == typeof(uint))
            {
                property.SetValue(obj, (uint)input.Id);
            }
            else if (propertyType == typeof(ushort))
            {
                property.SetValue(obj, (ushort)input.Id);
            }
            else if (propertyType == typeof(short))
            {
                property.SetValue(obj, (short)input.Id);
            }
            else
            {
                throw new ApplicationException($"Failed to bind {property.Name} of type {propertyType.Name}");
            }
        }

        private static void BindValueFromString<T>(string input, PropertyInfo property, T obj)
        {
            var propertyType = property.PropertyType;

            if (string.IsNullOrWhiteSpace(input))
            {
                property.SetValue(obj, GetDefault(property.PropertyType));
                return;
            }

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
            else if (propertyType == typeof(short))
            {
                var success = short.TryParse(input, out short parsed);
                if (!success)
                {
                    throw new ApplicationException($"Failed to parse {property.Name} into type short");
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
            else if (propertyType == typeof(double))
            {
                var success = double.TryParse(input, out double parsed);
                if (!success)
                {
                    throw new ApplicationException($"Failed to parse {property.Name} into type double");
                }

                property.SetValue(obj, parsed);
            }
            else if (propertyType == typeof(long))
            {
                var success = long.TryParse(input, out long parsed);
                if (!success)
                {
                    throw new ApplicationException($"Failed to parse {property.Name} into type long");
                }

                property.SetValue(obj, parsed);
            }
            else
            {
                throw new ApplicationException($"Unsupported binding type {propertyType.Name}");
            }
        }

        private static object GetDefault(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (MissingMethodException)
            {
                return null;
            }
        }
    }
}

