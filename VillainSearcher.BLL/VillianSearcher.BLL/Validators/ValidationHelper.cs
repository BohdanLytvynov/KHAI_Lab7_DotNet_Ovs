﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillianSearcher.BLL.Validators
{
    public static class ValidationHelper
    {
        public static bool IsNSLValid(string value, out string error, string defValue = "enter smth")
        {
            error = string.Empty;

            if (string.IsNullOrEmpty(value)) return false;

            if (value.Equals(defValue))
            {
                error = "Enter value";
                return false;
            }

            int length = value.Length;

            for (int i = 0; i < length; i++)
            {
                if (!Char.IsLetter(value[i]))
                {
                    error = $"Symbol in position {i + 1} is not a letter!";
                    return false;
                }

                if (i == 0 && !Char.IsUpper(value[i]))
                {
                    error = $"First letter must be in upper case!";
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateInt(int value, int start, int end, out string error)
        {
            error = string.Empty;

            if (value >= start && value <= end)
                return true;
            else
            {
                error = "Incorrect range!";
                return false;
            }
        }

        public static bool ValidateNumber(string value, out string error, string defValue = "enter smth")
        {
            error = string.Empty;

            if (string.IsNullOrEmpty(value)) return false;

            if (value.Equals(defValue))
            {
                error = "Enter value";
                return false;
            }

            int len = value.Length;

            for (int i = 0; i < len; i++)
            {
                if (!Char.IsDigit(value[i]))
                {
                    error = $"Symbol in position {i + 1} is not a digit!";
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateText(string value, out string error, string defValue = "enter smth")
        {
            error = string.Empty;
            if (string.IsNullOrEmpty(value)) return false;

            if (value.Equals(defValue))
            {
                error = "Enter value";
                return false;
            }

            int len = value.Length;
            for (int i = 0; i < len; i++)
            {
                if (!Char.IsLetter(value[i]))
                {
                    error = $"Incorrect symbol at: {i + 1}!";
                    return false;
                }
            }

            return true;
        }
    }
}
