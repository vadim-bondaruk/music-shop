namespace Shop.BLL.Helpers
{
    using System;
    using System.IO;
    using System.Web;

    /// <summary>
    /// </summary>
    public static class Mp3StreamHelper
    {
        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public static MemoryStream GetAudioStream(
            HttpResponseBase response,
            HttpRequestBase request,
            string trackName,
            string trackArtistName,
            byte[] audio)
        {
            if (audio == null)
            {
                return null;
            }

            long fSize = audio.Length;
            long startbyte = 0;
            var endbyte = fSize - 1;
            var statusCode = 200;
            if (request.Headers["Range"] != null)
            {
                var range = request.Headers["Range"].Split('=', '-');
                startbyte = Convert.ToInt64(range[1]);
                if (range.Length > 2 && range[2] != string.Empty)
                {
                    endbyte = Convert.ToInt64(range[2]);
                }

                if (startbyte != 0 || endbyte != fSize - 1 || range.Length > 2 && range[2] == string.Empty)
                {
                    statusCode = 206;
                }
            }

            var desSize = endbyte - startbyte + 1;
            response.ContentType = "audio/mp3";
            GetResponse(response, statusCode, trackArtistName, trackName, desSize, startbyte, endbyte, fSize);

            return new MemoryStream(audio, (int)startbyte, (int)desSize);
        }

        /// <summary>
        /// </summary>
        /// <param name="response">
        ///     The response.
        /// </param>
        /// <param name="statusCode">
        ///     The status code.
        /// </param>
        /// <param name="trackArtistName">
        ///     The track artist name.
        /// </param>
        /// <param name="trackName">
        ///     The track name.
        /// </param>
        /// <param name="desSize">
        ///     The des size.
        /// </param>
        /// <param name="startbyte">
        ///     The startbyte.
        /// </param>
        /// <param name="endbyte">
        ///     The endbyte.
        /// </param>
        /// <param name="fSize">
        ///     The f size.
        /// </param>
        private static void GetResponse(
            HttpResponseBase response,
            int statusCode,
            string trackArtistName,
            string trackName,
            long desSize,
            long startbyte,
            long endbyte,
            long fSize)
        {
            response.StatusCode = statusCode;
            response.AddHeader("TrackArtist", trackArtistName);
            response.AddHeader("TrackTitle", trackName);
            response.AddHeader("Content-Accept", response.ContentType);
            response.AddHeader("Content-Length", desSize.ToString());
            response.AddHeader("Content-Range", $"bytes {startbyte}-{endbyte}/{fSize}");
        }
    }
}