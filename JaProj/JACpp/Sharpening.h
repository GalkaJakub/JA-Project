#pragma once

#include <comdef.h>


extern "C" __declspec(dllexport) void ImageSharpening(unsigned char* data, int width, int height, int stride);
