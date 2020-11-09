using System;
using System.Collections.Generic;
using h73.Elastic.Core.Data;

namespace h73.Elastic.Core.Tests.Support
{
    public class IndexedClass
    {
        public int SomeNumber { get; set; }
        public Guid ObjectId { get; set; }
        public string AString { get; set; }
        public string BString { get; set; }
        public string CString { get; set; }
        public DateTime ADate { get; set; }
        public IndexedClass Child { get; set; }
        public List<IndexedClass> Children { get; set; }
        public IndexedClass2 Child2 { get; set; }
        public List<string> List { get; set; }
        public List<object> ListObjects { get; set; }
        public List<DateTime> ListDates { get; set; }
        public MetaData<IndexedClass> MetaData { get; set; }
        public MetaData<IndexedClass2> MetaData2 { get; set; }
    }

    public class IndexedClass2 : IndexedClass
    {
        public List<IndexedClass21> IndexedClass21S { get; set; }
    }

    public class IndexedClass3 : IndexedClass
    {
    }

    public class IndexedClass21 : IndexedClass2
    {

    }
}