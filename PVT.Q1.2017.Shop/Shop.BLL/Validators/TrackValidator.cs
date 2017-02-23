using System;
using Shop.BLL.Exceptions;
using Shop.Common.Models;
using Shop.Infrastructure.Validators;

namespace Shop.BLL.Validators
{
    public class TrackValidator : IValidator<Track>
    {
        #region Fields

        private readonly IValidator<Artist> _artistValidator;
        private readonly IValidator<Album> _albumValidator;

        #endregion //Fields

        #region Constructors

        public TrackValidator(IValidator<Artist> artistValidator, IValidator<Album> albumValidator)
        {
            if (artistValidator == null)
            {
                throw new ArgumentNullException(nameof(artistValidator));
            }
            if (albumValidator == null)
            {
                throw new ArgumentNullException(nameof(albumValidator));
            }
            _artistValidator = artistValidator;
            _albumValidator = albumValidator;
        }

        #endregion //Constructors

        #region Public Methods

        public void Validate(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            if (!IsTrackNameValid(track.Name))
            {
                throw new InvalidTrackException("Invalid track name specified.");
            }

            _artistValidator.Validate(track.Artist);

            if (track.Album != null)
            {
                _albumValidator.Validate(track.Album);
            }
        }

        public bool IsValid(Track track)
        {
            if (track == null)
            {
                return false;
            }

            if (!IsTrackNameValid(track.Name))
            {
                return false;
            }

            ArtistValidator artistValidator = new ArtistValidator();
            if (!artistValidator.IsValid(track.Artist))
            {
                return false;
            }

            if (track.Album != null)
            {
                AlbumValidator albumValidator = new AlbumValidator();
                if (!albumValidator.IsValid(track.Album))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsTrackNameValid(string trackName)
        {
            return !string.IsNullOrWhiteSpace(trackName);
        }

        #endregion //Public Methods
    }
}