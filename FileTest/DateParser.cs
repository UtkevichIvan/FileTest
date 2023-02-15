using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileTest
{
    public class DateParser
    {
        public void Get()
        {
            string path = "257.csv";
            string patternInt = @"^\d+$";
            string patternFloat = @"-?\d{1,3}\.\d+";
            using (StreamReader reader = new StreamReader(path))
            {
                using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Data3.csv"))
                {
                    while (reader.Peek() != -1)
                    {
                        int amountElements = 1;
                        int startIndex = 0;
                        string? textLine = reader.ReadLine();
                        int index = textLine.IndexOf(',', startIndex);
                        if (index != -1)
                        {
                            if (textLine.AsSpan().Slice(startIndex, index - startIndex).ToString() != "GSM")
                            {
                                continue;
                            }

                            amountElements++;
                            startIndex = index + 1;
                        }

                        var line = new StringBuilder();
                        bool check = true;
                        while (textLine.IndexOf(',', startIndex) != -1 && check)
                        {
                            index = textLine.IndexOf(',', startIndex);
                            if (amountElements == 2 || amountElements == 3 || amountElements == 4 || amountElements == 5)
                            {
                                var world = textLine.AsSpan().Slice(startIndex, index - startIndex).ToString();
                                if (Regex.IsMatch(world, patternInt))
                                {
                                    line.Append(world);
                                    if (amountElements != 8)
                                    {
                                        line.Append(',');
                                    }
                                }
                                else
                                {
                                    check = false;
                                    break;
                                }
                            }
                            else if (amountElements == 7 || amountElements == 8)
                            {
                                var world = textLine.AsSpan().Slice(startIndex, index - startIndex).ToString();
                                if (Regex.IsMatch(world, patternFloat))
                                {
                                    line.Append(world);
                                    if (amountElements != 8)
                                    {
                                        line.Append(',');
                                    }
                                }
                                else
                                {
                                    check = false;
                                    break;
                                }
                            }

                            amountElements++;
                            startIndex = index + 1;
                        }

                        if (check)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}