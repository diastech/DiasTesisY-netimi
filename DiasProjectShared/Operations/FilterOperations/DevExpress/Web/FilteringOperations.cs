using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DiasShared.Operations.FilteringOperation.DevExpress.Web
{
    public static class FilteringOperations
    {
        /// <summary>
        /// İkili filtre expression JSArrayini üretir
        /// </summary>
        /// <param name="filterFirst"></param>
        /// <param name="filterSecond"></param>
        /// <param name="andOr"> iki filtre arasındaki operator true -> and, false -> or</param>
        /// <returns>ProduceDevExpressFilterWithMultiFilterObject ile kullanılmalıdır</returns>
        public static object[] ProduceDevExpressFilterMultiDoubleArray(string[] filterFirst, string[] filterSecond, bool andOr = true)
        {
            if ((filterFirst != null) && (filterFirst.Length == 3) &&
                 (filterSecond != null) && (filterSecond.Length == 3))
            {
                object[] resultArrayInArray = new object[3];
                object[] filterObjectFirst = new object[3];

                for (int i = 0; i < filterObjectFirst.Length; i++)
                {
                    filterObjectFirst[i] = filterFirst[i];
                }

                resultArrayInArray[0] = filterObjectFirst;

                if (andOr) { resultArrayInArray[1] = "and"; }
                else { resultArrayInArray[1] = "or"; }

                resultArrayInArray[2] = filterSecond;

                return resultArrayInArray;
            }

            return null;
        }

        /// <summary>
        /// Çoklu filterların tek bir JSArray haline dönüştürüldüğü metod
        /// </summary>
        /// <param name="filterList"></param>
        /// <param name="andOrList">iki filtre arasındaki operatorler true -> and, false -> or</param>
        /// <param name="multiAndOr">ikili filtre arasındaki operatör  true -> and, false -> or</param>
        /// <returns>ProduceDevExpressFilterWithSingleFilterObject ile kullanılmalıdır</returns>
        public static object[] ProduceDevExpressFilterMultiArray(List<object[]> filterList, List<bool> andOrList = null, bool multiAndOr = true)
        {
            if((filterList != null) && (filterList.Count > 1) &&
                ((andOrList == null) || (andOrList.Count == filterList.Count -1)))
            {
                object[] resultListArrayInArray;

                if((filterList.Count % 2) == 0)
                {
                    resultListArrayInArray = new object[filterList.Count - 1];
                }
                else
                {
                    resultListArrayInArray = new object[filterList.Count];
                }               

                for (int i = 0; i < filterList.Count; i = i + 2) 
                {
                    object[] resultArrayInArray = new object[3];
                    object[] filterObjectFirst = new object[3];

                    for (int j = 0; j < filterList[i].Length; j++)
                    {
                        filterObjectFirst[j] = filterList[i][j];
                    }

                    if ((i <= filterList.Count - 2) || (filterList.Count % 2 == 0))//son tekli değil
                    {                       
                        if ((filterList[i] != null) && (filterList[i].Length == 3) &&
                             ((filterList[i + 1] != null) && (filterList[i + 1].Length == 3)))
                        {
                            resultArrayInArray[0] = filterObjectFirst;

                            if (andOrList[i]) { resultArrayInArray[1] = "and"; }
                            else { resultArrayInArray[1] = "or"; }

                            resultArrayInArray[2] = filterList[i + 1];
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else//Son tekli
                    {                      
                        resultArrayInArray = filterObjectFirst;
                    }                   

                    //return edeceğimiz diziyi üret
                    if (i >= filterList.Count - 2) //son ikili veya tekli
                    {
                        resultListArrayInArray[i] = resultArrayInArray;
                    }
                    else
                    {
                        resultListArrayInArray[i] = resultArrayInArray;

                        if (multiAndOr)
                        {
                            resultListArrayInArray[i + 1] = "and";
                        }
                        else
                        {
                            resultListArrayInArray[i + 1] = "or";
                        }
                    }
                }

                return resultListArrayInArray;
            }
           
            return null;           
        }

        /// <summary>
        /// İkili filtre expressionlarında kullanılmalıdır (A) and/or (B) gibi
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <param name="filterObject">filterObject en fazla 3 elemanlı olmalıdır</param>
        /// <returns>Hata döndürmez, hata olursa null döndürür</returns>
        public static DataSourceLoadOptions ProduceDevExpressFilterWithMultiFilterObject(DataSourceLoadOptions loadOptions, object[] filterObject)
        {
            try
            {
                if ((loadOptions != null) && (filterObject != null) && (filterObject.Length == 3))
                {
                    if ((loadOptions.Filter != null) && (loadOptions.Filter.Count > 0))
                    {
                        //Devexpress filtresinin temelini JS array tanımlar
                        //Tüm filtre öğeleri JS array olamak zorundadır                     
                        //IList fixed size olduğundan yeni bir filter IList tanımlamamız gerek
                        DataSourceLoadOptions newLoadFilter = new();

                        //Filter IList ancak böyle initialize edilebilir
                        newLoadFilter.Filter = new List<object>();

                        newLoadFilter.Filter.Add(loadOptions.Filter);
                        JArray convertedFilter;

                        if (filterObject[0].GetType().Name != "Object[]")
                        {
                           convertedFilter = new JArray(filterObject);
                        }
                        else//Multi array filtre
                        {
                            JArray firstFilter = new JArray(filterObject[0]);                        
                            JArray secondFilter = new JArray(filterObject[2]);

                            object[] filterJArrayObject = new object[3] { firstFilter, filterObject[1].ToString(), secondFilter };
                            convertedFilter = new JArray(filterJArrayObject);
                        }

                        //TODO: İhtiyaç olduğunda buraya or, xor vesaire ekleyebilmeliyiz
                        newLoadFilter.Filter.Add("and");
                        newLoadFilter.Filter.Add(convertedFilter);

                        return newLoadFilter;
                    }
                    else
                    {
                        loadOptions.Filter = filterObject;
                        return loadOptions;
                    }                   
                }

                return null;
            }
            catch(Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Çoklu filterların tek bir JSArray haline dönüştürüldüğü filtrelerde kullanılmalıdır
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <param name="filterObject">Çoklu JSArray filtresi olmalıdır</param>
        /// <returns>Hata döndürmez, hata olursa null döndürür</returns>
        public static DataSourceLoadOptions ProduceDevExpressFilterWithSingleFilterObject(DataSourceLoadOptions loadOptions, object[] filterObject)
        {
            try
            {
                if ((loadOptions != null) && (filterObject != null) && (filterObject.Length == 1))
                {
                    if ((loadOptions.Filter != null) && (loadOptions.Filter.Count > 0))
                    {
                        //Devexpress filtresinin temelini JS array tanımlar
                        //Tüm filtre öğeleri JS array olamak zorundadır
                        //IList fixed size olduğundan yeni bir filter IList tanımlamamız gerek
                        DataSourceLoadOptions newLoadFilter = new();

                        //Filter IList ancak böyle initialize edilebilir
                        newLoadFilter.Filter = new List<object>();

                        newLoadFilter.Filter.Add(loadOptions.Filter);                       

                        //TODO: İhtiyaç olduğunda buraya or, xor vesaire ekleyebilmeliyiz
                        newLoadFilter.Filter.Add("and");
                        newLoadFilter.Filter.Add(filterObject);

                        return newLoadFilter;
                    }
                    else
                    {
                        loadOptions.Filter = filterObject;
                        return loadOptions;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
