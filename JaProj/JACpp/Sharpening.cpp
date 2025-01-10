#include "pch.h"
#include "Sharpening.h"
#include <algorithm>


// Function to apply a sharpening filter to a single pixel channel
void ApplyFilter(BYTE* data, BYTE* outData, int stride, int x, int y)
{
    // Calculate the index of the current pixel channel in the data array
    int index = y * stride + x;
    int result = 0;

    // 3x3 sharpening mask
    static const int mask[3][3] =
    {
        { 0, -1, 0 },
        { -1, 5, -1 },
        { 0, -1, 0 }
    };

    // Iterate over the 3x3 neighborhood of the current pixel
    for (int i = -1; i <= 1; ++i)
    {
        for (int j = -1; j <= 1; ++j)
        {
            // Calculate the index of the neighboring pixel channel
            int currentIndex = index + (i * stride) + (j * 4);

            //use the sharpening mask and sum
            result += data[currentIndex] * mask[i + 1][j + 1];
        }
    }

    // Clamp the result to range [0, 255]
    result = std::clamp(result, 0, 255);
    // Add the result to the output data array
    outData[index] = result;
}

// Function for image sharpening
void ImageSharpening(BYTE* data, BYTE* outData, int width, int height, int stride)
{
    // Loop through each pixel, excluding the border pixels
    for (int y = 1; y < height - 1; ++y) {
        for (int x = 4; x < (width - 1) * 4; x += 4) {
            for (int c = 0; c < 4; c++)
            {
                int currentX = x + c;
                // Apply the sharpening filter to the current pixel channel
                ApplyFilter(data, outData, stride, currentX, y);
            }
        }
    }
}