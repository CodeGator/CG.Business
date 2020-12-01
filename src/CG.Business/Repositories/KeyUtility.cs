using CG.Business.Properties;
using System;
using System.Numerics;

namespace CG.Business.Repositories
{
    /// <summary>
    /// This class utility contains logic related to key values.
    /// </summary>
    public static class KeyUtility
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method indicates whether the specified key value is empty, or not.
        /// </summary>
        /// <typeparam name="TKey">The type of associated key.</typeparam>
        /// <param name="key">The key to use for the operation.</param>
        /// <returns>True if the key is missing / null / default.</returns>
        public static bool IsKeyMissing<TKey>(TKey key)
            where TKey : new()
        {
            var keyType = typeof(TKey);

            // Is the key a numeric type?
            if (keyType == typeof(short) ||
                keyType == typeof(ushort) ||
                keyType == typeof(byte) ||
                keyType == typeof(int) ||
                keyType == typeof(long) ||
                keyType == typeof(float) ||
                keyType == typeof(double) ||
                keyType == typeof(decimal) ||
                keyType == typeof(BigInteger)
                )
            {
                // Is the key value 0 ?
                return 0 == (double)Convert.ChangeType(key, typeof(double));
            }

            // Is the key a string type?
            if (keyType == typeof(string))
            {
                // Is the key empty, or NULL?
                return string.IsNullOrEmpty($"{key}");
            }

            // Is the key a GUID type?
            if (keyType == typeof(Guid))
            {
                // Try to parse the key
                if (false == Guid.TryParse($"{key}", out var g))
                {
                    return true;
                }

                // Is the value default?
                return g.Equals(Guid.Empty);
            }

            // Is the key a DateTime type?
            if (keyType == typeof(DateTime))
            {
                // Try to parse the key
                if (false == Guid.TryParse($"{key}", out var dt))
                {
                    return true;
                }

                // Is the value default?
                return dt.Equals(DateTime.MinValue) || dt.Equals(DateTime.MaxValue);
            }

            // Is the key a TimeSpan type?
            if (keyType == typeof(TimeSpan))
            {
                // Try to parse the key
                if (false == Guid.TryParse($"{key}", out var ts))
                {
                    return true;
                }

                // Is the value default?
                return ts.Equals(DateTime.MinValue) || ts.Equals(DateTime.MaxValue);
            }

            // Is the key an object type?
            if (keyType.IsClass)
            {
                // Is the value null?
                return key == null;
            }

            // If we get here then the key is of a type that we don't know how to 
            //   deal with, so, we'll panic!
            throw new InvalidOperationException(
                message: string.Format(
                    Resources.KeyUtility_KeyType,
                    keyType.Name
                    )
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method generates random key values for various well-known .NET 
        /// types.
        /// </summary>
        /// <typeparam name="TKey">The type of associated key.</typeparam>
        /// <returns></returns>
        public static TKey CreateRandomKey<TKey>()
            where TKey : new()
        {
            var keyType = typeof(TKey);

            // Is the key a numeric type?
            if (keyType == typeof(short) ||
                keyType == typeof(ushort) ||
                keyType == typeof(byte) ||
                keyType == typeof(int) ||
                keyType == typeof(long) ||
                keyType == typeof(float) ||
                keyType == typeof(double) ||
                keyType == typeof(decimal) ||
                keyType == typeof(BigInteger)
                )
            {
                // Return a pseudo random value.
                return (TKey)Convert.ChangeType(
                    new RandomEx().Next(),
                    keyType
                    );
            }

            // Is the key a string type?
            if (keyType == typeof(string))
            {
                // Return a pseudo random value.
                return (TKey)Convert.ChangeType(
                    new RandomEx().Next(),
                    keyType
                    );
            }

            // Is the key a GUID type?
            if (keyType == typeof(Guid))
            {
                // Return a random guid.
                return (TKey)Convert.ChangeType(
                    Guid.NewGuid(),
                    keyType
                    );
            }

            // Is the key a DateTime type?
            if (keyType == typeof(DateTime))
            {
                // Return a pseudo random datetime value.
                return (TKey)Convert.ChangeType(
                    new DateTime(
                        new RandomEx().Next(
                            (int)DateTime.MaxValue.Ticks, 
                            (int)DateTime.MaxValue.Ticks
                            ),
                        DateTimeKind.Utc
                        ),
                    keyType
                    );
            }

            // Is the key a TimeSpan type?
            if (keyType == typeof(TimeSpan))
            {
                // Return a pseudo random datetime value.
                return (TKey)Convert.ChangeType(
                    TimeSpan.FromTicks(
                        new RandomEx().Next(
                            (int)TimeSpan.MaxValue.Ticks,
                            (int)TimeSpan.MaxValue.Ticks
                            )
                        ),
                    keyType
                    );
            }

            // If we get here then the key is of a type that we don't know how to 
            //   deal with, so, we'll panic!
            throw new InvalidOperationException(
                message: string.Format(
                    Resources.KeyUtility_KeyType,
                    keyType.Name
                    )
                );
        }

        #endregion
    }
}
