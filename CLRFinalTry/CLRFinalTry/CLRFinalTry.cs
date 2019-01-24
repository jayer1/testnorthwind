using System;
using System.Collections;
using System.Text;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

namespace CLRFinalTry2
{
    public static class CLRFinalTry
    {
        //SQL Functions require an additional "SqlFunction" Attribute.
        //This attribute provides SQL server with additional meta data information it needs
        //regarding our custom function. In this example we are not accessing any data, and our
        //function is deterministic. So we let SQL know those facts about our function with
        //the DataAccess and IsDeterministic parameters of our attribute.
        //Additionally, SQL needs to know the name of a function it can defer to when it needs
        //to convert the object we have returned from our function into a structure that SQL
        //can understand. This is provided by the "FillRowMethodName" shown below.
        [SqlFunction(
            DataAccess = DataAccessKind.None,
            FillRowMethodName = "MyFillRowMethod"
            , IsDeterministic = true)
        ]
        //SQL Functions must be declared as Static. Table Valued functions must also
        //return a class that implements the IEnumerable interface. Most built in
        //.NET collections and arrays already implement this interface.
        public static IEnumerable Split2(string stringToSplit, string delimiters)
        {
            //One line of C# code splits our string on one or more delimiters...
            //A string array is one of many objects that are returnable from
            //a SQL CLR function - as it implements the required IEnumerable interface.
            string[] elements = stringToSplit.Split(delimiters.ToCharArray());
            return elements;
        }
        //SQL needs to defer to user code to translate the an IEnumerable item into something
        //SQL Server can understand. In this case we convert our string to a SqlChar object...
        public static void MyFillRowMethod(Object theItem, out SqlChars results)
        {
            results = new SqlChars(theItem.ToString());
        }
    }
}
