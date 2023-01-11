using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using PaylocityDemo.Entity;
using PaylocityDemo.Model.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Model.Benefit
{
    public class BenefitDiscountCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="benefit"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public virtual IPayrollLineItem CalculateDiscountItem(Entity.Benefit benefit, BenefitDiscount discount)
        {
            return new PayrollItem
            {
                Amount = benefit.Cost * (discount.PercentOff / 100m),
                Description = $"{benefit.Name} discount (-{discount.PercentOff.ToString("0.00")}%)",
            };
        }

        /// <summary>
        /// Determines if the given entity object qualifies for a discount.
        /// A discount can apply to an Employee or Dependent and the Expression field of a discount
        /// determines what to check and compare. Here I am using a string-to-expression evaluation
        /// </summary>
        /// <typeparam name="T">Entity type (i.e. Employee or Dependent)</typeparam>
        /// <param name="entity">The entity object</param>
        /// <param name="discount">The benefit discount object</param>
        /// <returns></returns>
        public virtual bool Qualifies<T>(T entity, BenefitDiscount discount) where T : class
        {
            var entities = new List<T>() { entity };
            var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);
            Func<T, bool> expr = CSharpScript.EvaluateAsync<Func<T, bool>>(discount.Expression, options).Result;
            return entities.Where(expr).Any();
        }
    }
}
