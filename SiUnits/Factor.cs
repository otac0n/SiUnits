// Copyright © John & Katie Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SiUnits
{
    /// <summary>
    /// The base clas for factors in SI Units.
    /// </summary>
    public abstract class Factor
    {
        internal Factor()
        {
        }

        /// <summary>
        /// Multiplies two factors.
        /// </summary>
        /// <param name="left">The left factor to multiply.</param>
        /// <param name="right">The right factor to multiply.</param>
        /// <returns>The multiplied factor.</returns>
        public static Factor operator *(Factor left, Factor right) => Multiply(left, right);

        /// <summary>
        /// Divides one factor by another.
        /// </summary>
        /// <param name="left">The factor to divide.</param>
        /// <param name="right">The factor to divide by.</param>
        /// <returns>The combined factor.</returns>
        public static Factor operator /(Factor left, Factor right) => Multiply(left, right.Pow(-1));

        /// <summary>
        /// Multiplies a collection of factors.
        /// </summary>
        /// <param name="factors">The collection of factors to multiply.</param>
        /// <returns>The multiplied factor.</returns>
        public static Factor Multiply(params Factor[] factors)
        {
            var factor = new CompositeFactor(factors);
            return factor.Factors.Count == 1 ? factor.Factors[0] : factor;
        }

        /// <summary>
        /// Raises the factor to the specified power.
        /// </summary>
        /// <param name="power">The power to which this factor should be raised.</param>
        /// <returns>A new factor equal to this factor raised to the specified power.</returns>
        public abstract Factor Pow(int power);

        /// <inheritdoc />
        public abstract override string ToString();
    }
}
