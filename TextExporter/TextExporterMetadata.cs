using System;
using System.Collections.Generic;
using System.Composition;
using OpenSense.Component.Contract;
using OpenSense.Component.Psi;

namespace OpenSense.Component.TextExporter {
    [Export(typeof(IComponentMetadata))]
    public class TextExporterMetadata : IComponentMetadata {

        public string Name => "Text File Exporter";

        public string Description => @"Write streams to a Text file.";

        public IReadOnlyList<IPortMetadata> Ports => new[] {
            new ExporterPortMetadata(),
        };

        public ComponentConfiguration CreateConfiguration() => new TextExporterConfiguration();

        public object GetConnector<T>(object instance, PortConfiguration portConfiguration) => throw new InvalidOperationException();
    }
}