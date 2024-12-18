#pragma once

#include <comdef.h>


extern "C" __declspec(dllexport) void ImageSharpening(BYTE * data, int width, int height, int stride);
