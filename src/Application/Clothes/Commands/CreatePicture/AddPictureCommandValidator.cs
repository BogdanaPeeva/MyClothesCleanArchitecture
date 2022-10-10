using System;
using FluentValidation;

namespace MyClothesCA.Application.Clothes.Commands.CreatePicture
{
    public class AddPictureCommandValidator : AbstractValidator<AddPictureCommandValidator>
    {
        public AddPictureCommandValidator()
        {
            //RuleFor(v => v.Title)
            //.MaximumLength(200)
            //.NotEmpty();
        }
    }
}

