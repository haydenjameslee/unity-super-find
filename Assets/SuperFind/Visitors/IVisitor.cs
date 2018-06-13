namespace SuperFindPlugin
{
    public interface IVisitor
    {
        void Visit(INode node);
        bool ShortCircuit();
    }
}