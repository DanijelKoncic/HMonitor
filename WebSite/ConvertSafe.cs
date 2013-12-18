using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite
{
    /// <summary>
    /// Klasa za konverziju podataka bez exceptiona.
    /// </summary>    

    public class ConvertSafe
    {
        #region ToInt32: String
            /// <summary>
            /// Konverzija stringa u Int32.
            /// </summary>
            /// <param name="value">String vrijednost koja se konvertira.</param>
            /// <returns>Int32 vrijednost ili 0 ukoliko konverzija nije uspješno izvršena.</returns>
            public static int ToInt32(string value)
            {
                try 
			    { 
				    if (!string.IsNullOrEmpty(value)) 
					    return System.Convert.ToInt32(value); 
			    }
                catch {}
                return 0;
            }
            #endregion

            #region ToInt32 : object
            /// <summary>
            /// Konverzija objekta u Int32.
            /// </summary>
            /// <param name="value">String vrijednost koja se konvertira.</param>
            /// <returns>Int32 vrijednost ili 0 ukoliko konverzija nije uspješno izvršena.</returns>
            public static int ToInt32(object value)
            {
                try
                {
            	    if (value != null) 
					    return System.Convert.ToInt32(value);
                }
                catch {}
                return 0;
            }
            #endregion

            #region ToInt32 : String
            /// <summary>
            /// Konverzija stringa u Int32 sa statusom uspješnosti.
            /// </summary>
            /// <param name="value">String vrijednost koja se konvertira.</param>
            /// <param name="result">Int32 konvertirana vrijednost. 0 ukoliko konverzija nije uspješno izvršena.</param>
            /// <returns>True ukoliko je konverzija uspješno izvršena, u suprotnom False.</returns>
            public static bool ToInt32(string value, out int result)
            {
                try
                {
            	    if (!string.IsNullOrEmpty(value))
                    {
                        result = System.Convert.ToInt32(value);
                        return true;
                    }
            	    result = 0;
                }
                catch { result = 0; }
                return false;
            }
            #endregion

		    #region ToByte: String
		    /// <summary>
		    /// Konverzija stringa u Byte.
		    /// </summary>
		    /// <param name="value">String vrijednost koja se konvertira.</param>
		    /// <returns>Byte vrijednost ili 0 ukoliko konverzija nije uspješno izvršena.</returns>
		    public static byte ToByte(string value)
		    {
			    try { if (!string.IsNullOrEmpty(value)) return System.Convert.ToByte(value); }
			    catch { }
			    return 0;
		    }
		    #endregion

		    #region ToByte: object
		    /// <summary>
		    /// Konverzija stringa u Byte.
		    /// </summary>
		    /// <param name="value">String vrijednost koja se konvertira.</param>
		    /// <returns>Byte vrijednost ili 0 ukoliko konverzija nije uspješno izvršena.</returns>
		    public static byte ToByte(object value)
		    {
			    try { if (value != null) return System.Convert.ToByte(value); }
			    catch { }
			    return 0;
		    }
		    #endregion

		    #region ToShort: Object
		    /// <summary>
		    /// Konverzija objekta u Short.
		    /// </summary>
		    /// <param name="value">Objekt vrijednost koja se konvertira.</param>
		    /// <returns>Short vrijednost ili 0 ukoliko konverzija nije uspješno izvršena.</returns>
		    public static short ToShort(object value)
		    {
			    try { if (value != null) return System.Convert.ToInt16(value); }
			    catch { }
			    return 0;
		    }
		    #endregion

		    #region ToString: String
		    /// <summary>
		    /// Konverzija stringa u string sa kontrolom null stringa.
		    /// </summary>
		    /// <param name="value">String vrijednost koja se konvertira.</param>
		    /// <returns>String vrijednost ili prazan string ukoliko konverzija nije uspješno izvršena.</returns>
		    public static string ToString(string value)
		    {
			    try { 
				    return !string.IsNullOrEmpty(value) ? value : String.Empty; 
			    }
			    catch { }
			    return string.Empty;
		    }
		    #endregion

		    #region ToBool: Object
		    /// <summary>
		    /// Konverzija objekta u bool.
		    /// </summary>
		    /// <param name="value">Objekt vrijednost koja se konvertira.</param>
		    /// <returns>Bool vrijednost ili false ukoliko konverzija nije uspješno izvršena.</returns>
		    public static bool ToBool(object value)
		    {
			    try { if (value != null) return System.Convert.ToBoolean(value); }
			    catch { }
			    return false;
		    }
		    #endregion

		    #region ToDecimal i ToDouble: Object
		    /// <summary>
		    /// Konverzija objekta u Nullable decimal.
		    /// </summary>
		    /// <param name="value">Objekt vrijednost koja se konvertira.</param>
		    /// <returns>Nullable decimal vrijednost ili 0 ukoliko konverzija nije uspješno izvršena.</returns>
		    public static decimal ToDecimal(object value)
		    {
			    try { if (value != null) return System.Convert.ToDecimal(value); }
			    catch { }
			    return 0;
		    }
		    /// <summary>
		    /// Konverzija objekta u Nullable double.
		    /// </summary>
		    /// <param name="value">Objekt vrijednost koja se konvertira.</param>
		    /// <returns>Nullable double vrijednost ili 0 ukoliko konverzija nije uspješno izvršena.</returns>
		    public static double ToDouble(object value)
    	    {
			    try { if (value != null) return System.Convert.ToDouble(value); }
			    catch { }
			    return 0;
		    }
		    #endregion
    }
}

