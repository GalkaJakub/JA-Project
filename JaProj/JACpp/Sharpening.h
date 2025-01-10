#pragma once

#include <comdef.h>


extern "C" __declspec(dllexport) void ImageSharpening(BYTE * data, BYTE * outData, int width, int height, int stride);
