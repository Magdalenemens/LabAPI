using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Common
{
    public class NumberToWords
    {
        private static String[] units = { "Zero", "One", "Two", "Three",
    "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
    "Seventeen", "Eighteen", "Nineteen" };
        private static String[] tens = { "", "", "Twenty", "Thirty", "Forty",
    "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public static String ConvertAmount(double amount)
        {
            try
            {
                Int64 amount_int = (Int64)amount;
                Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return ConvertPlus(amount_int) + " Riyal Only.";
                }
                else
                {
                    return ConvertPlus(amount_int) + " Riyal And " + ConvertPlus(amount_dec) + " Halala Only.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public static String ConvertPlus(Int64 i)
        {
            if (i < 20)
            {
                return units[i];
            }
            if (i < 100)
            {
                return tens[i / 10] + ((i % 10 > 0) ? " " + ConvertPlus(i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " Hundred"
                        + ((i % 100 > 0) ? " And " + ConvertPlus(i % 100) : "");
            }
            if (i < 100000)
            {
                return ConvertPlus(i / 1000) + " Thousand "
                        + ((i % 1000 > 0) ? " " + ConvertPlus(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return ConvertPlus(i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + ConvertPlus(i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return ConvertPlus(i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + ConvertPlus(i % 10000000) : "");
            }
            return ConvertPlus(i / 1000000000) + " Arab "
                    + ((i % 1000000000 > 0) ? " " + ConvertPlus(i % 1000000000) : "");
        }

        private static String[] unitsAr = { "صفر", "واحد", "اثنين", "ثلاثة","أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "أحد عشر", "اثنا عشر", "أربعة عشر", "خمسة عشر", "ستة عشر",    "سبعة عشر", "ثمانية عشر", "تسعة عشر" };
        private static String[] tensAr = { "", "", "عشرين", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };

        public static String ConvertAmountAr(double amount)
        {
            try
            {
                Int64 amount_int = (Int64)amount;
                Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return ConvertPlusAr(amount_int) + " فقط.";
                }
                else
                {
                    return ConvertPlusAr(amount_int) + " نقطة " + ConvertPlusAr(amount_dec) + " فقط.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public static String ConvertPlusAr(Int64 i)
        {
            if (i < 20)
            {
                return unitsAr[i];
            }
            if (i < 100)
            {
                return tensAr[i / 10] + ((i % 10 > 0) ? " " + ConvertPlus(i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " مائة"
                        + ((i % 100 > 0) ? " And " + ConvertPlus(i % 100) : "");
            }
            if (i < 100000)
            {
                return ConvertPlus(i / 1000) + " ألف "
                        + ((i % 1000 > 0) ? " " + ConvertPlus(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return ConvertPlus(i / 100000) + " لكح "
                        + ((i % 100000 > 0) ? " " + ConvertPlus(i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return ConvertPlus(i / 10000000) + " الكرور عشرة ملا يين "
                        + ((i % 10000000 > 0) ? " " + ConvertPlus(i % 10000000) : "");
            }
            return ConvertPlus(i / 1000000000) + " عربي "
                    + ((i % 1000000000 > 0) ? " " + ConvertPlus(i % 1000000000) : "");
        }





        //public static string ConvertNumberToWords(int pValue, string pLanguage)

        //{

        //    string strReturn;

        //    if (pValue < 0)

        //        throw new NotSupportedException("Negative numbers not supported");

        //    else if (pValue == 0)

        //        strReturn = pLanguage == ".en" ? "Zero" : "صفر";

        //    else if (pValue < 10)

        //        strReturn = ConvertDigitToWords(pValue, pLanguage);

        //    else if (pValue < 20)

        //        strReturn = ConvertTeensToWords(pValue, pLanguage);

        //    else if (pValue < 100)

        //        strReturn = ConvertHighTensToWords(pValue, pLanguage);

        //    else if (pValue < 1000)

        //        strReturn = ConvertBigNumberToWords(pValue, 100, "Hundred", pLanguage);

        //    else if (pValue < 1000000)

        //        strReturn = ConvertBigNumberToWords(pValue, 1000, "Thousand", pLanguage);

        //    else if (pValue < 1000000000)

        //        strReturn = ConvertBigNumberToWords(pValue, 1000000, "Million", pLanguage);

        //    else

        //        throw new NotSupportedException("Number is too large!!!");

        //    if (pLanguage == ".ar")

        //    {

        //        if (strReturn.EndsWith("quatre-vingt"))

        //        {

        //            //another French exception

        //            //strReturn += "s";

        //        }

        //    }

        //    return strReturn;

        //}

        //private static string ConvertDigitToWords(int pValue, string pLanguage)

        //{

        //    switch (pValue)

        //    {

        //        case 0: return "";

        //        case 1: return pLanguage == ".en" ? "One" : "واحد";

        //        case 2: return pLanguage == ".en" ? "Two" : "اثنين";

        //        case 3: return pLanguage == ".en" ? "Three" : "ثلاثة";

        //        case 4: return pLanguage == ".en" ? "Four" : "أربعة";

        //        case 5: return pLanguage == ".en" ? "Five" : "خمسة";

        //        case 6: return pLanguage == ".en" ? "six" : "ستة";

        //        case 7: return pLanguage == ".en" ? "Seven" : "سبعة";

        //        case 8: return pLanguage == ".en" ? "Eight" : "ثمانية";

        //        case 9: return pLanguage == ".en" ? "Nine" : "تسعة";

        //        default:

        //            throw new IndexOutOfRangeException($"{pValue} not a digit");

        //    }

        //}

        ////assumes a number between 10 & 19

        //private static string ConvertTeensToWords(int pValue, string pLanguage)

        //{

        //    switch (pValue)

        //    {

        //        case 10: return pLanguage == ".en" ? "Ten" : "عشرة";

        //        case 11: return pLanguage == ".en" ? "Eleven" : "أحد عشر";

        //        case 12: return pLanguage == ".en" ? "Twelve" : "اثنا عشر";

        //        case 13: return pLanguage == ".en" ? "thirteen" : "ثلاثة عشر";

        //        case 14: return pLanguage == ".en" ? "Fourteen" : "أربعة عشر";

        //        case 15: return pLanguage == ".en" ? "Fifteen" : "خمسة عشر";

        //        case 16: return pLanguage == ".en" ? "Sixteen" : "ستة عشر";

        //        case 17: return pLanguage == ".en" ? "Seventeen" : "سبعة عشر";

        //        case 18: return pLanguage == ".en" ? "Eighteen" : "ثمانية عشر";

        //        case 19: return pLanguage == ".en" ? "Nineteen" : "تسعة عشر";

        //        default:

        //            throw new IndexOutOfRangeException($"{pValue} not a teen");

        //    }

        //}

        ////assumes a number between 20 and 99

        //private static string ConvertHighTensToWords(int pValue, string pLanguage)

        //{

        //    int tensDigit = (int)(Math.Floor((double)pValue / 10.0));

        //    string tensStr;

        //    switch (tensDigit)

        //    {

        //        case 2: tensStr = pLanguage == ".en" ? "Twenty" : "عشرين"; break;

        //        case 3: tensStr = pLanguage == ".en" ? "Thirty" : "ثلاثون"; break;

        //        case 4: tensStr = pLanguage == ".en" ? "Forty" : "أربعون"; break;

        //        case 5: tensStr = pLanguage == ".en" ? "Fifty" : "خمسون"; break;

        //        case 6: tensStr = pLanguage == ".en" ? "Sixty" : "ستون"; break;

        //        case 7: tensStr = pLanguage == ".en" ? "Seventy" : "سبعون"; break;

        //        case 8: tensStr = pLanguage == ".en" ? "Eighty" : "ثمانون"; break;

        //        case 9: tensStr = pLanguage == ".en" ? "Ninety" : "تسعون"; break;

        //        default:

        //            throw new IndexOutOfRangeException($"{pValue} not in range 20-99");

        //    }

        //    if (pValue % 10 == 0) return tensStr;

        //    //French sometime has a prefix in front of 1

        //    string strPrefix = string.Empty;

        //    if (pLanguage == ".ar" && (tensDigit < 8) && (pValue - tensDigit * 10 == 1))

        //        strPrefix = "-et";

        //    string onesStr;

        //    if (pLanguage == ".ar" && (tensDigit == 7 || tensDigit == 9))

        //    {

        //        tensStr = ConvertHighTensToWords(10 * (tensDigit - 1), pLanguage);

        //        onesStr = ConvertTeensToWords(10 + pValue - tensDigit * 10, pLanguage);

        //    }

        //    else

        //        onesStr = ConvertDigitToWords(pValue - tensDigit * 10, pLanguage);

        //    return tensStr + strPrefix + "-" + onesStr;

        //}

        //// Use this to convert any integer bigger than 99

        //private static string ConvertBigNumberToWords(int pValue, int baseNum, string baseNumStr, string pLanguage)

        //{

        //    // special case: use commas to separate portions of the number, unless we are in the hundreds

        //    string separator;

        //    if (pLanguage == ".ar")

        //        separator = " ";

        //    else

        //        separator = (baseNumStr != "Hundred") ? ", " : " ";

        //    // Strategy: translate the first portion of the number, then recursively translate the remaining sections.

        //    // Step 1: strip off first portion, and convert it to string:

        //    int bigPart = (int)(Math.Floor((double)pValue / baseNum));

        //    string bigPartStr;

        //    if (pLanguage == ".ar")

        //    {

        //        string baseNumStrFrench;

        //        switch (baseNumStr)

        //        {

        //            case "Hundred":

        //                baseNumStrFrench = "مائة";

        //                break;

        //            case "Thousand":

        //                baseNumStrFrench = "ألف";

        //                break;

        //            case "Million":

        //                baseNumStrFrench = "مليون";

        //                break;

        //            case "Billion":

        //                baseNumStrFrench = "مليار";

        //                break;

        //            default:

        //                baseNumStrFrench = "????";

        //                break;

        //        }

        //        if (bigPart == 1 && pValue < 1000000)

        //            bigPartStr = baseNumStrFrench;

        //        else

        //            bigPartStr = ConvertNumberToWords(bigPart, pLanguage) + " " + baseNumStrFrench;

        //    }

        //    else

        //        bigPartStr = ConvertNumberToWords(bigPart, pLanguage) + " " + baseNumStr;

        //    // Step 2: check to see whether we're done:

        //    if (pValue % baseNum == 0)

        //    {

        //        if (pLanguage == ".ar")

        //        {

        //            if ((bigPart > 1) && (baseNumStr == "Million"))

        //            {

        //                //in French, a s is required to cent/mille/million/milliard if there is a value in front but nothing after

        //                return bigPartStr + "s"; //This is not required as always dirhams is suffixed in the end

        //                //return bigPartStr;

        //            }

        //            else

        //                return bigPartStr;

        //        }

        //        else

        //            return bigPartStr;

        //    }

        //    else

        //    {

        //        if (pLanguage == ".ar")

        //        {

        //            if ((bigPart > 1) && (baseNumStr == "Million"))

        //            {

        //                //in French, a s is required to cent/mille/million/milliard if there is a value in front but nothing after

        //                bigPartStr = bigPartStr + "s"; //This is not required as always dirhams is suffixed in the end

        //                //return bigPartStr;

        //            }

        //        }

        //    }

        //    // Step 3: concatenate 1st part of string with recursively generated remainder:

        //    int restOfNumber = pValue - bigPart * baseNum;

        //    return bigPartStr + separator + ConvertNumberToWords(restOfNumber, pLanguage);

        //}


        public static string arabicNumber(double num)
        {
            string[] aname = { "واحد", "اثنان", "ثلاثة", "اربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "أحد عشر", "اثنا عشر" };
            string[] aname10 = { "عشر", "عشرون", "ثلاثون", "اربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };
            string[] aname100 = { "مئة", "مئتان", "ثلثمائة", "اربعمائة", "خمسمائة", "ستمائة", "سبعمائة", "ثمانمائة", "تسعمائة" };
            string[] aname1000 = { "الف", "الفان" };


            int num4 = Convert.ToInt32(Math.Floor((num) / 1000));
            int num3 = Convert.ToInt32(Math.Floor((num - 1000 * num4) / 100));
            int num2 = Convert.ToInt32(Math.Floor((num - 100 * num3) / 10));
            int num1 = Convert.ToInt32(num - 10 * num2);

            if (num4 == 0 && num > 999)
                return aname1000[num4 - 1];
            if (num4 > 2 && num4 < 11)
                return aname[num4 - 1] + " و " + arabicNumber(num - 1000 * num4);
            if (num4 > 2 && num4 > 10)
                return arabicNumber(num4 - 1) + " و " + arabicNumber(num - 1000 * num4);
            if (num4 < 3 && num > 1000)
                return aname1000[num4 - 1] + "و" + arabicNumber(num - 1000 * num4);
            if (num3 == 0 && num > 99)
                return aname100[num3 - 1];
            if (num3 != 0 && num > 100)
                return aname100[num3 - 1] + " و " + arabicNumber(num - 100 * num3);
            if (num > 12 && num < 20)
                return aname[num1 - 1] + aname10[num2 - 1];
            if (num > 20 && num % 10 != 0)
                return aname[num1 - 1] + " و " + aname10[num2 - 1];
            if (num < 13)
                return aname[Convert.ToInt32(num) - 1];
            if (num > 19 && num % 10 == 0)
                return aname10[num2 - 1];
            return num.ToString();
        }
    }
}
