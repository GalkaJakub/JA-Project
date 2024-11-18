#include "pch.h"
#include "Sharpening.h"
#include <iostream>
#include <fstream>

// Function for image sharpening
void ImageSharpening(unsigned char* data, int width, int height, int stride)
{
    // 3x3 sharpening mask
    int mask[3][3] =
    {
        { 0, -1, 0 },
        { -1, 5, -1 },
        { 0, -1, 0 }
    };
    // Loop through each pixel, excluding the border pixels
    for (int y = 1; y < height - 1; ++y) {
        unsigned char* row = data + y * stride;
        unsigned char* nextRow = data + (y + 1) * stride;
        unsigned char* prevRow = data + (y - 1) * stride;
        for (int x = 3; x < (width - 1) * 3; x += 3) {
            int resultRed = row[x] * mask[1][1] + prevRow[x - 3] * mask[0][0] + prevRow[x] * mask[0][1] + prevRow[x + 3] * mask[0][2] +
                row[x - 3] * mask[1][0] + row[x + 3] * mask[1][2] +
                nextRow[x - 3] * mask[2][0] + nextRow[x] * mask[2][1] + nextRow[x + 3] * mask[2][2];

            int resultGreen = row[x + 1] * mask[1][1] + prevRow[x - 2] * mask[0][0] + prevRow[x + 1] * mask[0][1] + prevRow[x + 4] * mask[0][2] +
                row[x - 2] * mask[1][0] + row[x + 4] * mask[1][2] +
                nextRow[x - 2] * mask[2][0] + nextRow[x + 1] * mask[2][1] + nextRow[x + 4] * mask[2][2];

            int resultBlue = row[x + 2] * mask[1][1] + prevRow[x - 1] * mask[0][0] + prevRow[x + 2] * mask[0][1] + prevRow[x + 5] * mask[0][2] +
                row[x - 1] * mask[1][0] + row[x + 5] * mask[1][2] +
                nextRow[x - 1] * mask[2][0] + nextRow[x + 2] * mask[2][1] + nextRow[x + 5] * mask[2][2];

            // Function to clamp pixel values to the range [0, 255]
            auto clamp = [](int value, int minVal, int maxVal) {
                return (value < minVal) ? minVal : (value > maxVal ? maxVal : value);
                };

            // Assign the new clamped pixel values to the corresponding channels
            row[x] = clamp(resultRed, 0, 255);
            row[x + 1] = clamp(resultGreen, 0, 255);
            row[x + 2] = clamp(resultBlue, 0, 255);

        }
    }
}