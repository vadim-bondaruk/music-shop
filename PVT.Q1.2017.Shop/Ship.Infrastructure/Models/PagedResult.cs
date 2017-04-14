namespace Shop.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the paged results.
    /// </summary>
    /// <remarks>
    /// Idea is taken from Gunnar Peipman's ASP.NET blog: https://weblogs.asp.net/gunnarpeipman/returning-paged-results-from-repositories-using-pagedresult-lt-t-gt
    /// </remarks>
    public class PagedResult<T> where T: BaseEntity
    {
        private readonly ICollection<T> _items;
        private readonly int _pagesCount;
        private readonly int _pageSize;
        private readonly int _currentPage;

        public PagedResult(ICollection<T> items, int pageSize, int page)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this._items = items;

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "Incorrect page size specified");
            }

            _pageSize = pageSize;

            _pagesCount = (int)Math.Ceiling((double)_items.Count / _pageSize);

            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), page, "Incorrect current page value specified");
            }

            _currentPage = page > _pagesCount ? _pagesCount : page;
        }

        public ICollection<T> Items
        {
            get { return this._items; }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
        }

        public int PagesCount
        {
            get { return _pagesCount; }
        }

        public int PageSize
        {
            get { return _pageSize; }
        }

        public int ItemsCount
        {
            get { return _items.Count; }
        }
    }
}