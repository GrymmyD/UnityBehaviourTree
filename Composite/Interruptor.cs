using SSG.BehaviourTrees.Primitives;

namespace SSG.BehaviourTrees.Composites
{
    public class Interruptor<T> : Composite<T> where T : BehaviourState
    {
        private int currentChild = 0;

        /// <summary>
        /// Nodes are executed in order, later nodes have higher priority so if they can be executed they are.
        /// Last node is uninterruptable.
        /// A lower priority node's state changes persist if not overwritten by higher priority nodes.
        /// </summary>
        /// <param name="compositeName"></param>
        /// <param name="nodes"></param>
        public Interruptor(string compositeName, params Node<T>[] nodes) : base(compositeName, nodes)
        {
        }

        public override NodeStatus OnBehave(T state)
        {
            NodeStatus ret = children[currentChild].Behave(state);

            switch (ret)
            {
                case NodeStatus.SUCCESS:
                case NodeStatus.FAILURE:
                    Reset();
                    return NodeStatus.SUCCESS;

                case NodeStatus.RUNNING:
                    for (int i = currentChild + 1; i < children.Count; i++)
                    {
                        switch (children[i].Behave(state))
                        {
                            case NodeStatus.SUCCESS:
                                return NodeStatus.SUCCESS;
                            case NodeStatus.RUNNING:
                                currentChild = i;
                                break;
                        }
                    }
                    break;
            }
            return NodeStatus.RUNNING;
        }

        public override void OnReset()
        {
            currentChild = 0;
            foreach (Node<T> child in children)
            {
                child.Reset();
            }
        }
    }
}