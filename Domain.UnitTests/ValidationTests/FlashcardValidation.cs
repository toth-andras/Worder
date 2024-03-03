// "WORDER" Client-server Application for Learning Foreign Words, 2023-24
// Andras Toth
// Created: 23.2.2024

using Domain.Flashcards;
using Domain.Validation;

namespace Domain.UnitTests.ValidationTests;

public class FlashcardValidation
{
    private FlashcardValidator validator = new();

    [Fact(DisplayName = "Term validation")]
    public void TermValidationTest()
    {
        TextStyle textStyle = new() { Color="White", Font = "Ariel", FontSize = 12};
        Flashcard flashcard = new () {Definition = "Definition", TermStyle = textStyle, DefinitionStyle = textStyle};

        // Term is null.
        var validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotNullValidator", validationResult.Errors[0].ErrorCode);
        
        // Term is empty.
        flashcard.Term = string.Empty;
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotEmptyValidator", validationResult.Errors[0].ErrorCode);
        
        // Term is whitespace.
        flashcard.Term = "              ";
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotEmptyValidator", validationResult.Errors[0].ErrorCode);
        
        // Term is too long.
        flashcard.Term = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commod"; // 71 character
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("LengthValidator",validationResult.Errors[0].ErrorCode);
        
        // Term is correct.
        flashcard.Term = "This is a term";
        validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }
    
    [Fact(DisplayName = "Definition validation")]
    public void DefinitionValidationTest()
    {
        TextStyle textStyle = new() { Color="White", Font = "Ariel", FontSize = 12};
        Flashcard flashcard = new () {Term = "Term", TermStyle = textStyle, DefinitionStyle = textStyle};

        // Definition is null.
        var validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotNullValidator", validationResult.Errors[0].ErrorCode);
        
        // Definition is empty.
        flashcard.Definition = string.Empty;
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotEmptyValidator", validationResult.Errors[0].ErrorCode);
        
        // Definition is whitespace.
        flashcard.Definition = "              ";
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotEmptyValidator", validationResult.Errors[0].ErrorCode);
        
        // Definition is too long.
        flashcard.Definition = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget" +
                         " dolor. Aenean ma"; // 101 character
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("LengthValidator",validationResult.Errors[0].ErrorCode);
        
        // Definition is correct.
        flashcard.Definition = "This is a definition";
        validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Fields validation")]
    public void FieldsValidationTest()
    {
        // Fields list is null.
        TextStyle textStyle = new() { Color="White", Font = "Ariel", FontSize = 12};
        Flashcard flashcard = new () 
            {Term = "Term", Definition = "Definition", TermStyle = textStyle, DefinitionStyle = textStyle};
        var validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
        
        // Fields list is empty.
        flashcard.Fields = [];
        validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
        
        // Fields list only contains correct fields.
        flashcard.Fields = [
            new TextFlashcardField() {Name = "Name", Value = "Value", Style = textStyle},
            new TextFlashcardField() {Name = "Name2", Value = "Value2", Style = textStyle}
        ];
        validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
        
        // Fields list contains incorrect field.
        flashcard.Fields = [
            new TextFlashcardField() {Name = "Name", Value = "Value", Style = textStyle},
            new TextFlashcardField() {Name = "Name2", Value = "Value2", Style = null}
        ];
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotNullValidator", validationResult.Errors[0].ErrorCode);
        
        // Fields list contains incorrect fields.
        flashcard.Fields = [
            new TextFlashcardField() {Name = null, Value = "Value", Style = textStyle},
            new TextFlashcardField() {Name = "Name2", Value = "Value2", Style = null}
        ];
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotNullValidator", validationResult.Errors[0].ErrorCode);
    }

    [Fact(DisplayName = "Tag validation")]
    public void TagValidationTest()
    {
        TextStyle textStyle = new() { Color="White", Font = "Ariel", FontSize = 12};
        Flashcard flashcard = new () 
            {Term = "Term", Definition = "Definition", TermStyle = textStyle, DefinitionStyle = textStyle};
        
        // Tags list is null.
        var validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
        
        // Tags list is empty.
        flashcard.Tags = new List<Tag>();
        validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
        
        // Tags list only has correct tags.
        flashcard.Tags = [new Tag() {Name = "Tag"}, new Tag() {Name = "Tag #2"}];
        validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
        
        // Tags list has incorrect tag.
        Tag incorrectTag = new Tag();
        flashcard.Tags = [new Tag() {Name = "Tag"}, incorrectTag];
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotNullValidator", validationResult.Errors[0].ErrorCode);
        
        // Tags list has multiple incorrect tags.
        flashcard.Tags = [new Tag() {Name = string.Empty}, incorrectTag];
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotEmptyValidator", validationResult.Errors[0].ErrorCode);
    }

    [Fact(DisplayName = "Term style validation")]
    public void TermStyleValidation()
    {
        TextStyle textStyle = new() { Color="White", Font = "Ariel", FontSize = 12};
        Flashcard flashcard = new () 
            {Term = "Term", Definition = "Definition", TermStyle = textStyle, DefinitionStyle = textStyle};
        
        // Term style is null.
        flashcard.TermStyle = null;
        var validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotNullValidator", validationResult.Errors[0].ErrorCode);
        
        // Term style is incorrect.
        flashcard.TermStyle = new TextStyle() {Font = String.Empty};
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Equal(3, validationResult.Errors.Count);
        Assert.Equal("NotEmptyValidator", validationResult.Errors[0].ErrorCode);
        Assert.Equal("GreaterThanValidator", validationResult.Errors[1].ErrorCode);
        Assert.Equal("NotNullValidator", validationResult.Errors[2].ErrorCode);
        
        // Term style is correct.
        flashcard.TermStyle = flashcard.DefinitionStyle;
        validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }
    
    [Fact(DisplayName = "Definition style validation")]
    public void DefinitionStyleValidation()
    {
        TextStyle textStyle = new() { Color="White", Font = "Ariel", FontSize = 12};
        Flashcard flashcard = new () 
            {Term = "Term", Definition = "Definition", TermStyle = textStyle, DefinitionStyle = textStyle};
        
        // Definition style is null.
        flashcard.DefinitionStyle = null;
        var validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
        Assert.Equal("NotNullValidator", validationResult.Errors[0].ErrorCode);
        
        // Definition style is incorrect.
        flashcard.DefinitionStyle = new TextStyle() {Font = String.Empty};
        validationResult = validator.Validate(flashcard);
        Assert.False(validationResult.IsValid);
        Assert.Equal(3, validationResult.Errors.Count);
        Assert.Equal("NotEmptyValidator", validationResult.Errors[0].ErrorCode);
        Assert.Equal("GreaterThanValidator", validationResult.Errors[1].ErrorCode);
        Assert.Equal("NotNullValidator", validationResult.Errors[2].ErrorCode);
        
        // Definition style is correct.
        flashcard.DefinitionStyle = flashcard.TermStyle;
        validationResult = validator.Validate(flashcard);
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }
}