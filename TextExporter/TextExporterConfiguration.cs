using System;
using System.IO;
using Microsoft.Psi;
using OpenSense.Component.Contract;
using OpenSense.Component.Psi;

namespace OpenSense.Component.TextExporter {
    [Serializable]
    public class TextExporterConfiguration : ExporterConfiguration {

        private string filename = Path.GetTempFileName();

        public string Filename {
            get => filename;
            set => SetProperty(ref filename, value);
        }

        private int maxRecursionDepth = int.MaxValue;

        public int MaxRecursionDepth {
            get => maxRecursionDepth;
            set => SetProperty(ref maxRecursionDepth, value);
        }

        private string nullValueResultString = "null";

        public string NullValueResultString {
            get => nullValueResultString;
            set => SetProperty(ref nullValueResultString, value);
        }

        public override IComponentMetadata GetMetadata() => new TextExporterMetadata();

        protected override object CreateInstance(Pipeline pipeline) {
            var result = new TextExporter(pipeline, Filename) { 
                MaxRecursionDepth = MaxRecursionDepth,
                NullValueResultString = NullValueResultString,
            };
            return result;
        }

        protected override void ConnectInput<T>(object instance, InputConfiguration inputConfiguration, IProducer<T> remoteEndProducer) {
            var streamName = (string)inputConfiguration.LocalPort.Index;
            var exporter = (TextExporter)instance;
            exporter.WriteStream((dynamic)remoteEndProducer, streamName, inputConfiguration.DeliveryPolicy);
        }

        protected override void FinalizeInstantiation(object instance) {
            var exporter = (TextExporter)instance;
            exporter.FinishInstantiation();
        }
    }
}