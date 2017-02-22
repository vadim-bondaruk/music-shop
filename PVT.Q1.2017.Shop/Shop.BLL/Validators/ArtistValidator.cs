using System;
using Shop.Common.Models;
using Shop.Infrastructure.Validators;

namespace Shop.BLL.Validators
{
    public class ArtistValidator : IValidator<Artist>
    {
        public void Validate(Artist artist)
        {
            throw new NotImplementedException();
        }

        public Boolean IsValid(Artist artist)
        {
            throw new NotImplementedException();
        }
    }
}