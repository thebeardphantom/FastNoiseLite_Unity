## Modifications

The following modifications have been made to the original FastNoiseLite project:
- Renamed FastNoiseLite class to NoiseGenerator
- Moved files into FastNoise namespace.
- Split code file into various categorized partial classes.
- Adjusted naming conventions to closer match Microsoft's C# naming conventions.
- Added INoiseGenerator interface.
- Added GetNoise01 extension methods to get noise in the \[0, 1\] range.
- Added methods to offset seed before sampling noise.
- Added NoiseGeneratorConfig class for serialized generator settings.
- Added NoiseGeneratorAsset class as a ScriptableObject based generator.
- Added NoiseGeneratorRng class to act as a random-access random number generator driven by a noise generator instance.

## License for Modifications

These modifications are released under the MIT License, with the following copyright notice:

Copyright 2023 Mika Notarnicola

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.