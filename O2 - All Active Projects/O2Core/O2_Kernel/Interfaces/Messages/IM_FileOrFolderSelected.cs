namespace O2.Kernel.Interfaces.Messages
{
    public interface IM_FileOrFolderSelected : IO2Message
    {
        string pathToFileOrFolder { get; set; }
        int lineNumber { get; set; }
    }
}