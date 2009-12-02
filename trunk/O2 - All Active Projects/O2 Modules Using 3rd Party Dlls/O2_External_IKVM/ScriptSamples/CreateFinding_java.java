import cli.O2.Kernel.PublicDI;
import cli.O2.Kernel.Interfaces.*;
import cli.O2.DotNetWrappers.O2Findings.*;
import cli.O2.ImportExport.OunceLabs.Ozasmt_OunceV6.*;
import cli.O2.Kernel.Interfaces.O2Findings.TraceType;


class CreateFinding
{  
	public static void main(String args[])
	{
		System.out.println("!!Creating Custom finding file using O2");
		
		String o2KernelDll = PublicDI.get_config().get_O2KernelAssemblyName();
		
		System.out.println("This is an O2 call from Java: " +  o2KernelDll);		
		
		O2Assessment o2Assessment = new O2Assessment();

		O2Finding finding = new O2Finding();
		finding.set_vulnName("hello world");
		finding.set_vulnType("really bad");

		O2Trace root_trace = new O2Trace("root");
		O2Trace source = new O2Trace("source");
		
		source.set_traceType(TraceType.wrap(2)); // TraceType.Source // this TraceType Enum was a pain to get since TraceType tt = TraceType.Source; doesn't work

		root_trace.get_childTraces().Add(source);
		O2Trace sink_trace = new O2Trace("sink");
		sink_trace.set_traceType(TraceType.wrap(3)); // Interfaces.Ozasmt.TraceType.Known_Sink;

		root_trace.get_childTraces().Add(sink_trace);

		finding.get_o2Traces().Add(root_trace);

		o2Assessment.get_o2Findings().Add(finding);
		String tempfile = o2Assessment.save(new O2AssessmentSave_OunceV6());

		System.out.println("saved assessment was saved to " + tempfile);		
	}
}