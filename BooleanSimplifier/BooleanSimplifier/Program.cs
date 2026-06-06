using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BooleanSimplifier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("   Boolean Expression Simplifier");
            Console.WriteLine("=================================");
            Console.WriteLine("Syntax: + = OR,  * or · = AND,  ' or ! = NOT");
            Console.WriteLine("Variables: A-Z,  Constants: 0, 1");
            Console.WriteLine("Type 'exit' to quit.\n");

            while (true)
            {
                Console.Write("Enter expression: ");
                string input = Console.ReadLine();
                if (input == null || input.Trim().ToLower() == "exit") break;
                if (string.IsNullOrWhiteSpace(input)) continue;

                string normalized = Normalize(input);
                var steps = Simplify(normalized);

                Console.WriteLine();
                Console.WriteLine($"  Input      : {input.Trim()}");
                Console.WriteLine($"  Simplified : {steps[steps.Count - 1].Expr}");
                Console.WriteLine();

                if (steps.Count == 1)
                {
                    Console.WriteLine("  [No further simplification found — already minimal]\n");
                }
                else
                {
                    Console.WriteLine("  Steps:");
                    for (int i = 0; i < steps.Count; i++)
                    {
                        if (i == 0)
                            Console.WriteLine($"    [0] {steps[i].Expr}  <- Input");
                        else
                            Console.WriteLine($"    [{i}] {steps[i].Expr}  <- {steps[i].Law}");
                    }
                    Console.WriteLine();
                }
            }
        }

        static string Normalize(string s)
        {
            s = s.Trim();
            s = s.Replace(" ", "");
            s = s.Replace("*", "·");
            s = s.Replace("!", "'");
            return s;
        }

        static List<Step> Simplify(string expr)
        {
            var steps = new List<Step> { new Step(expr, "Input") };
            string current = expr;
            int maxIter = 20;

            for (int i = 0; i < maxIter; i++)
            {
                Step result = ApplyRules(current);
                if (result == null || result.Expr == current) break;
                steps.Add(result);
                current = result.Expr;
            }

            return steps;
        }

        static Step ApplyRules(string e)
        {
            Func<string, Step>[] rules = {
                DoubleNegation,
                ComplementLaw,
                NullLaw,
                IdentityLaw,
                IdempotentLaw,
                AbsorptionLaw,
                SpecialCase1,
                SpecialCase2,
                DeMorganLaw,
                DistributiveFactor,
                DistributeAND
            };

            foreach (var rule in rules)
            {
                var res = rule(e);
                if (res != null && res.Expr != e) return res;
            }
            return null;
        }

        // (A')' = A
        static Step DoubleNegation(string e)
        {
            var patterns = new[]
            {
                new Regex(@"\(([A-Z0-9])\)''"),
                new Regex(@"([A-Z])''"),
            };
            foreach (var pat in patterns)
            {
                var m = pat.Match(e);
                if (m.Success)
                {
                    string inner = m.Groups[1].Value;
                    string result = e.Substring(0, m.Index) + inner + e.Substring(m.Index + m.Length);
                    return new Step(result, "Double Negation — (A')' = A");
                }
            }
            return null;
        }

        // A + A' = 1,  A · A' = 0
        static Step ComplementLaw(string e)
        {
            string r;
            r = Regex.Replace(e, @"([A-Z])\+\1'", "1");
            if (r != e) return new Step(r, "Complement — A + A' = 1");

            r = Regex.Replace(e, @"([A-Z])'\+\1", "1");
            if (r != e) return new Step(r, "Complement — A' + A = 1");

            r = Regex.Replace(e, @"([A-Z])·\1'", "0");
            if (r != e) return new Step(r, "Complement — A · A' = 0");

            r = Regex.Replace(e, @"([A-Z])'·\1", "0");
            if (r != e) return new Step(r, "Complement — A' · A = 0");

            return null;
        }

        // A · 0 = 0,  A + 1 = 1
        static Step NullLaw(string e)
        {
            string r;
            r = Regex.Replace(e, @"([A-Z])·0|0·([A-Z])", "0");
            if (r != e) return new Step(r, "Null (Domination) — A · 0 = 0");

            r = Regex.Replace(e, @"([A-Z])\+1|1\+([A-Z])", "1");
            if (r != e) return new Step(r, "Null (Domination) — A + 1 = 1");

            return null;
        }

        // A + 0 = A,  A · 1 = A
        static Step IdentityLaw(string e)
        {
            string r;
            r = Regex.Replace(e, @"([A-Z])\+0|0\+([A-Z])", m =>
                !string.IsNullOrEmpty(m.Groups[1].Value) ? m.Groups[1].Value : m.Groups[2].Value);
            if (r != e) return new Step(r, "Identity — A + 0 = A");

            r = Regex.Replace(e, @"([A-Z])·1|1·([A-Z])", m =>
                !string.IsNullOrEmpty(m.Groups[1].Value) ? m.Groups[1].Value : m.Groups[2].Value);
            if (r != e) return new Step(r, "Identity — A · 1 = A");

            return null;
        }

        // A + A = A,  A · A = A
        static Step IdempotentLaw(string e)
        {
            string r;
            r = Regex.Replace(e, @"([A-Z])\+\1(?!')", m => m.Groups[1].Value);
            if (r != e) return new Step(r, "Idempotent — A + A = A");

            r = Regex.Replace(e, @"([A-Z])·\1(?!')", m => m.Groups[1].Value);
            if (r != e) return new Step(r, "Idempotent — A · A = A");

            return null;
        }

        // A + A·B = A,  A·(A+B) = A
        static Step AbsorptionLaw(string e)
        {
            string r;
            r = Regex.Replace(e, @"([A-Z])\+\1·([A-Z])", m => m.Groups[1].Value);
            if (r != e) return new Step(r, "Absorption — A + A·B = A");

            r = Regex.Replace(e, @"([A-Z])\+([A-Z])·\1", m => m.Groups[1].Value);
            if (r != e) return new Step(r, "Absorption — A + B·A = A");

            r = Regex.Replace(e, @"([A-Z])·\(\1\+([A-Z])\)", m => m.Groups[1].Value);
            if (r != e) return new Step(r, "Absorption — A·(A+B) = A");

            r = Regex.Replace(e, @"([A-Z])·\(([A-Z])\+\1\)", m => m.Groups[1].Value);
            if (r != e) return new Step(r, "Absorption — A·(B+A) = A");

            return null;
        }

        // A + A'·B = A + B
        static Step SpecialCase1(string e)
        {
            string r = Regex.Replace(e, @"([A-Z])\+([A-Z])'·([A-Z])", m =>
            {
                string a = m.Groups[1].Value, b = m.Groups[2].Value, c = m.Groups[3].Value;
                return a == b ? $"{a}+{c}" : m.Value;
            });
            if (r != e) return new Step(r, "Special Case — A + A'·B = A + B");

            r = Regex.Replace(e, @"([A-Z])·\(([A-Z])'\+([A-Z])\)", m =>
            {
                string a = m.Groups[1].Value, b = m.Groups[2].Value, c = m.Groups[3].Value;
                return a == b ? $"{a}·{c}" : m.Value;
            });
            if (r != e) return new Step(r, "Special Case — A·(A'+B) = A·B");

            return null;
        }

        // A' + A·B = A' + B
        static Step SpecialCase2(string e)
        {
            string r = Regex.Replace(e, @"([A-Z])'\+([A-Z])·([A-Z])", m =>
            {
                string a = m.Groups[1].Value, b = m.Groups[2].Value, c = m.Groups[3].Value;
                if (a == b) return $"{a}'+{c}";
                if (a == c) return $"{a}'+{b}";
                return m.Value;
            });
            if (r != e) return new Step(r, "Special Case — A' + A·B = A' + B");

            return null;
        }

        // (A·B)' = A'+B',  (A+B)' = A'·B'
        static Step DeMorganLaw(string e)
        {
            string r;
            r = Regex.Replace(e, @"\(([A-Z])·([A-Z])\)'", m =>
                $"{m.Groups[1].Value}'+{m.Groups[2].Value}'");
            if (r != e) return new Step(r, "DeMorgan's — (A·B)' = A' + B'");

            r = Regex.Replace(e, @"\(([A-Z])\+([A-Z])\)'", m =>
                $"{m.Groups[1].Value}'·{m.Groups[2].Value}'");
            if (r != e) return new Step(r, "DeMorgan's — (A+B)' = A'·B'");

            return null;
        }

        // A·B + A·C = A·(B+C)
        static Step DistributiveFactor(string e)
        {
            var orTerms = SplitOR(e);
            if (orTerms.Count < 2) return null;

            for (int i = 0; i < orTerms.Count; i++)
            {
                var ai = GetANDFactors(orTerms[i]);
                for (int j = i + 1; j < orTerms.Count; j++)
                {
                    var aj = GetANDFactors(orTerms[j]);
                    foreach (var factor in ai)
                    {
                        if (aj.Contains(factor))
                        {
                            var restI = new List<string>(ai); restI.Remove(factor);
                            var restJ = new List<string>(aj); restJ.Remove(factor);
                            string rI = restI.Count > 0 ? string.Join("·", restI) : "1";
                            string rJ = restJ.Count > 0 ? string.Join("·", restJ) : "1";
                            string newTerm = $"{factor}·({rI}+{rJ})";
                            var remaining = new List<string>(orTerms);
                            remaining.RemoveAt(j); remaining.RemoveAt(i);
                            remaining.Add(newTerm);
                            string newExpr = string.Join("+", remaining);
                            return new Step(newExpr, "Distributive — A·B + A·C = A·(B+C)");
                        }
                    }
                }
            }
            return null;
        }

        // A·(B+C) = A·B + A·C
        static Step DistributeAND(string e)
        {
            var m = Regex.Match(e, @"([A-Z])·\(([^)]+)\+([^)]+)\)");
            if (!m.Success) return null;
            string a = m.Groups[1].Value, b = m.Groups[2].Value, c = m.Groups[3].Value;
            string replacement = $"{a}·{b}+{a}·{c}";
            string result = e.Substring(0, m.Index) + replacement + e.Substring(m.Index + m.Length);
            return new Step(result, "Distributive — A·(B+C) = A·B + A·C");
        }

        static List<string> SplitOR(string e)
        {
            var terms = new List<string>();
            int depth = 0;
            string cur = "";
            foreach (char ch in e)
            {
                if (ch == '(') depth++;
                else if (ch == ')') depth--;
                if (ch == '+' && depth == 0) { terms.Add(cur); cur = ""; }
                else cur += ch;
            }
            if (cur.Length > 0) terms.Add(cur);
            return terms;
        }

        static List<string> GetANDFactors(string term)
        {
            var factors = new List<string>();
            int depth = 0;
            string cur = "";
            foreach (char ch in term)
            {
                if (ch == '(') depth++;
                else if (ch == ')') depth--;
                if (ch == '·' && depth == 0) { factors.Add(cur); cur = ""; }
                else cur += ch;
            }
            if (cur.Length > 0) factors.Add(cur);
            return factors;
        }
    }

    class Step
    {
        public string Expr { get; }
        public string Law { get; }
        public Step(string expr, string law) { Expr = expr; Law = law; }
    }
}