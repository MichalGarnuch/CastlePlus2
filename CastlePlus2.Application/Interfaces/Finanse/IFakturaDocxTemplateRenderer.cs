namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IFakturaDocxTemplateRenderer
    {
        byte[] Render(
            byte[] templateBytes,
            IReadOnlyDictionary<string, string> placeholders,
            IReadOnlyList<IReadOnlyDictionary<string, string>> itemRows);
    }
}