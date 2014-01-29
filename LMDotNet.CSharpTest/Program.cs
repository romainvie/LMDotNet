﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMDotNet;

namespace LMDotNet.CSharpTest
{

    class Program
    {
        // demo: intersect parabola with unit circle
        static void IntersectUnitCircleParabola(double[] parameters, double[] residuals) {
            // evaluate non-linear system/residuals based on
            // parameter vector p
            // unit circle        x^2 + y^2 = 1
            residuals[0] = parameters[0] * parameters[0] + parameters[1] * parameters[1] - 1.0;
            // standard parabola  y = x^2
            residuals[1] = parameters[1] - parameters[0] * parameters[0];                  
        }

        static void Main(string[] args) {
            var lmaSolver = new LMSolver(verbose: true);

            // First solution: start at (1.0, 1.0)
            var res1 = lmaSolver.Solve(Program.IntersectUnitCircleParabola, new[] { 1.0, 1.0 });
            // second solution: start at (-1.0, 1.0)
            var res2 = lmaSolver.Solve(Program.IntersectUnitCircleParabola, new[] { -1.0, 1.0 });

            Console.WriteLine();            
            Console.WriteLine("=============================================================");
            if (res1.outcome == SolverStatus.ConvergedBoth ||
                res1.outcome == SolverStatus.ConvergedParam ||
                res1.outcome == SolverStatus.ConvergedSumSq) {
                Console.WriteLine("1st solution: x = {0}, y = {1}", res1.optimizedParameters[0], res1.optimizedParameters[1]);
            }
            if (res2.outcome == SolverStatus.ConvergedBoth ||
                res2.outcome == SolverStatus.ConvergedParam ||
                res2.outcome == SolverStatus.ConvergedSumSq) {
                Console.WriteLine("2nd solution: x = {0}, y = {1}", res2.optimizedParameters[0], res2.optimizedParameters[1]);
            }

            Console.WriteLine();
            Console.WriteLine();

            // Alternative: use a lambda expression:
            var res3 = lmaSolver.Solve((p, r) => {
                r[0] = p[0] * p[0] + p[1] * p[1] - 1.0;            
                r[1] = p[1] - p[0] * p[0]; },
                new[] { 1.0, 1.0 });
                        
            Console.WriteLine("=============================================================");
            if (res3.outcome == SolverStatus.ConvergedBoth ||
                res3.outcome == SolverStatus.ConvergedParam ||
                res3.outcome == SolverStatus.ConvergedSumSq) {
                Console.WriteLine("1st solution: x = {0}, y = {1}", res3.optimizedParameters[0], res3.optimizedParameters[1]);
            }
        }
    }
}
