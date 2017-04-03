namespace PVT.Q1._2017.Shop.Areas.Management.Validators
{
    using FluentValidation;

    using global::Shop.Common.Models.ViewModels;

    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;

    /// <summary>
    ///     The <see cref="TrackManagementViewModel" /> validator
    /// </summary>
    public class TrackManagementValidator : AbstractValidator<TrackManagementViewModel>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackManagementValidator" /> class.
        /// </summary>
        public TrackManagementValidator()
        {
            this.RuleFor(t => t.Name).NotEmpty().WithMessage("Укажите название трека");
            this.RuleFor(t => t.Name).Matches(@"^\S+(\s\S+)*$").WithMessage("Название трека указано не корректно");

            this.RuleFor(t => t.TrackFile).NotNull().WithMessage("Укажите файл трека");

            this.RuleFor(t => t.Artist.Name).Empty().WithMessage("Не указан исполнитель трека");
        }
    }
}