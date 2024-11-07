namespace FluentOpenApiValidation
{
    public struct Security
    {
        public readonly string Name;
        public readonly Member Member;

        public Security(string name, Member member)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            Name = name;
            Member = member;
        }
    }

    public struct Member
    {
        public readonly string[] Items;

        public Member(string[] items)
        {
            ArgumentNullException.ThrowIfNull(items);

            Items = items;
        }
    }
}