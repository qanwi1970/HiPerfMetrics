using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HiPerfMetrics.Reports
{
    public class DeepXmlReport : IMetricReport
    {
        public HiPerfMetric Metric { get; set; }

        public string SummaryMessage
        {
            get { return Metric.SummaryMessage; }
        }

        public void WriteXmlReport(string fileName)
        {
            var serializer = new XmlSerializer(typeof (HiPerfMetric));
            serializer.Serialize(new StreamWriter(fileName), Metric);
        }

        public string StringXmlReport()
        {
            var writer = new StringWriter();
            var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings {Indent = false});
            var serializer = new XmlSerializer(typeof (HiPerfMetric));
            serializer.Serialize(xmlWriter, Metric);
            return writer.ToString();
        }
    }

    public static class DeepXmlReportExtensions
    {
        public static DeepXmlReport GetDeepXmlReport(this HiPerfMetric hiPerfMetric)
        {
            return new DeepXmlReport {Metric = hiPerfMetric};
        }

	    public static string ReportAsDeepXml(this HiPerfMetric hiPerfMetric)
	    {
		    var report = new DeepXmlReport {Metric = hiPerfMetric};
		    return report.StringXmlReport();
	    }
    }
}
