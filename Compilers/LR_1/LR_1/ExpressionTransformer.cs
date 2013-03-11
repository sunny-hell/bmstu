using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_1
{
    static class ExpressionTransformer
    {
        static public string transformToPostfixNotation(string input)
        {
            StringBuilder sb = new StringBuilder();
            Stack<char> stack = new Stack<char>();
            foreach (char c in input)
            {
                if (Char.IsLetterOrDigit(c))
                    sb.Append(c);
                else
                    switch (c)
                    {
                        case '(': stack.Push(c); break;
                        case ')':
                            while (stack.Peek() != '(')
                                sb.Append(stack.Pop());
                            stack.Pop();  // pop ')'
                            break;
                        case '^':
                            sb.Append(c);
                            break;
                        case '*':
                            while ((stack.Count != 0) && (stack.Peek() == '^'))
                                sb.Append(stack.Pop());
                            stack.Push(c);
                            break;
                        case '+':
                            while ((stack.Count != 0) && ((stack.Peek() == '*') || 
                                (stack.Peek() == '^')))
                                sb.Append(stack.Pop());
                            stack.Push(c);
                            break;     
                    }
            }
            int N = stack.Count;
            for (int i = 0; i < N; i++)
                sb.Append(stack.Pop());
            return sb.ToString();
 
        }
    }
}
