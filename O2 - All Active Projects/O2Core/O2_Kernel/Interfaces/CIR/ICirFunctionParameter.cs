namespace O2.Kernel.Interfaces.CIR
{
    public interface ICirFunctionParameter
    {
        string ParameterName { get; set; }
        string ParameterType { get; set; }
        string Constant { get; set; }
        bool HasConstant { get; set; }
        bool HasDefault { get; set; }
        string Method { get; set; }
        bool IsTainted { get; set; }
    }
}