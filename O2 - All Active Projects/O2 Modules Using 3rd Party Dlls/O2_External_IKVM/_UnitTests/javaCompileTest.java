import cli.O2.Kernel.*;
import cli.O2.Kernel.PublicDI.*;
//import cli.java.lang;

class JavaCompileTest
{  
	public static void main(String args[])
	{
		String o2KernelDll = PublicDI.get_config().get_O2KernelAssemblyName();
		System.out.println("O2 from Java test: " +  o2KernelDll);		
	}
}