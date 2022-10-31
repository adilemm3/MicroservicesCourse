namespace PlatformService.Domain.Models;

public abstract class Entity
{
    #region Equals

    public int Id { get; protected init; }

    public override bool Equals(object obj)
    {
        if (obj is not Entity)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        Entity item = (Entity)obj;

        return item.Id == this.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    public static bool operator ==(Entity left, Entity right)
    {
        return left?.Equals(right) ?? Equals(right, null);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }

    #endregion
}