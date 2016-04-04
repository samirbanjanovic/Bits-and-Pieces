public static string GenerateSequenceNumber()
{
    var guid = Guid.NewGuid().ToString("N");
    var ca = Encoding.Default.GetBytes(guid);
    var csa = string.Join("", ca);

    double byteGuid;
    if(double.TryParse(csa, out byteGuid))
    {
        byteGuid = byteGuid % DateTime.Now.ToOADate();

        var sequence = byteGuid.ToString().Replace(".", "");
    
        if (sequence.Length > 11)
        {
            sequence = sequence.Substring(0, 11);
        }
        else
        {
            sequence = sequence.PadLeft(11, '0');
        }
    
        return sequence;
    }

    return null;
}
