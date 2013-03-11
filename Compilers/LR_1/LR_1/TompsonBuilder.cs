using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_1
{
    class TompsonBuilder: BuilderFA
    {
        private FiniteAutomate buildOr(FiniteAutomate fa1, FiniteAutomate fa2)
        {
            FiniteAutomate fa = new FiniteAutomate();
            int shift1 = fa.add(fa1);
            int shift2 = fa.add(fa2);
            fa.InitState = fa.addState();
            fa.addTransition(fa.InitState, fa1.InitState+shift1, ' '); // epsilon-transition
            fa.addTransition(fa.InitState, fa2.InitState+shift2, ' ');
            fa.FinStates.Add(fa.addState());
            fa.addTransition(fa1.FinStates.First() + shift1, fa.FinStates.First(), ' ');
            fa.addTransition(fa2.FinStates.First() + shift2, fa.FinStates.First(), ' ');
            return fa;
        }

        private FiniteAutomate buildAnd(FiniteAutomate fa1, FiniteAutomate fa2)
        {
            FiniteAutomate fa = new FiniteAutomate();
            int shift1 = fa.add(fa1);
            int shift2 = fa.add(fa2);
            fa.InitState = fa1.InitState + shift1;
            fa.FinStates.Add(fa2.FinStates.First() + shift2);
            List<int> statesToUnion = new List<int>();
            statesToUnion.Add(fa1.FinStates.First() + shift1);
            statesToUnion.Add(fa2.InitState + shift2);
            fa.unionStates(statesToUnion);
            return fa;
        }

        public FiniteAutomate buildIteration(FiniteAutomate fa)
        {
            FiniteAutomate iterFA = new FiniteAutomate();
            int shift = iterFA.add(fa);
            iterFA.InitState = iterFA.addState();
            iterFA.addTransition(iterFA.InitState, fa.InitState + shift, ' ');
            iterFA.FinStates.Add(iterFA.addState());
            iterFA.addTransition(fa.FinStates.First() + shift, iterFA.FinStates.First(), ' ');
            iterFA.addTransition(fa.FinStates.First() + shift, fa.InitState + shift, ' ');
            iterFA.addTransition(iterFA.InitState, iterFA.FinStates.First(), ' ');
            return iterFA;
 
        }

        public override FiniteAutomate build(string regExpr)
        {
            // regExpr = string in postfix notation
            Stack<FiniteAutomate> stack = new Stack<FiniteAutomate>();
            foreach (char c in regExpr)
                if (Char.IsLetterOrDigit(c))
                    stack.Push(new FiniteAutomate(c));
                else
                    if (c == '^')
                        stack.Push(buildIteration(stack.Pop()));
                    else 
                    {
                        FiniteAutomate fa2 = stack.Pop();
                        FiniteAutomate fa1 = stack.Pop();
                        if (c == '+')
                            stack.Push(buildOr(fa1, fa2));
                        else
                            // c == '*'
                            stack.Push(buildAnd(fa1, fa2));
                    }
            FiniteAutomate result = stack.Pop();
            return result;
        }
    }
}
