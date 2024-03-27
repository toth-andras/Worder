// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 29.2.2024

namespace Migrations.TableDescriptors;

public static class TextStyleTable
{
    public static readonly string TableName = "text_styles";
    public static readonly string Id = "id";
    public static readonly string TextIsRightToLeft = "text_is_right_to_left";
    public static readonly string FontId = "font_id";
    public static readonly string FontSize = "font_size";
    public static readonly string ColorId = "color_id";
}