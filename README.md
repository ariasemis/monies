# Monies

![CI](https://github.com/ariasemis/monies/workflows/CI/badge.svg)

Yet another money class written in C#

This library aims to solve the following issues:

## Lossless monetary operations

Monetary calculations are often rounded to the smallest currency unit, but doing so you may end up losing or creating money because of rounding errors.

In C# this is somewhat alleviated by the `decimal` datatype, yet we can still have problems when dealing with currencies whose units are not in base 10 (like in precious metals for instance).

## Support for user-defined currency types

Most libraries come with a predefined set of currencies built-in (usually based on ISO 4217), but offer limited options for clients to define their own custom currencies.

This library aims to support any currency, including ISO 4217, cryptocurrencies, vendor-specific currencies, precious metals, etc.
