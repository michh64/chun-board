using System;
using System.Collections.Generic;
using System.Collections;

namespace ChunBoard
{
    public class Variable
    {
        public static ArrayList rootarr = new ArrayList();
        public static List<ArrayList> brancharr = new List<ArrayList>();
        //public static ArrayList dataarr = new ArrayList();
        public static List<ArrayList> popularity = new List<ArrayList>();

        public static string storageFile = @"..\..\4 DATA\data.txt";
        public static string contributeFile = @"..\..\\4 DATA\ContributeData.txt";


        public static string ver;
        public static string[] punctuation;
        public static string[] numb;

        public static ArrayList Error = new ArrayList();
        public static bool[] CheckList = new bool[100];

        public static string[] ArrStr;

        // Điều khiển số từ hiển thị
        public static int display = 5;

        public static int index;


    }
}
