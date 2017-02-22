﻿using System;
using Shop.BLL.Exceptions;
using Shop.Common.Models;
using Shop.Infrastructure.Validators;

namespace Shop.BLL.Validators
{
    public class TrackValidator : IValidator<Track>
    {
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

            ArtistValidator artistValidator = new ArtistValidator();
            artistValidator.Validate(track.Artist);

            if (track.Album != null)
            {
                AlbumValidator albumValidator = new AlbumValidator();
                albumValidator.Validate(track.Album);
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