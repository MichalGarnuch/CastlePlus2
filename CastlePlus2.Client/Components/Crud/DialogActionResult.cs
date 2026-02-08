namespace CastlePlus2.Client.Components.Crud;

public enum DialogAction
{
    Edit
}

public sealed record DialogActionResult(DialogAction Action);