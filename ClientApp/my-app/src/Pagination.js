import React, { useState, useEffect } from "react";

export const usePagination = ({ contentPerPage, count }) => {
  const [page, setPage] = useState(1);
  const [gaps, setGaps] = useState({
    before: false,
    paginationGroup: [],
    after: true,
  });

  const pageCount = Math.ceil(count / contentPerPage);
  const lastContentIndex = page * contentPerPage;
  const firstContentIndex = lastContentIndex - contentPerPage;
  const [pagesInBetween, setPagesInBetween] = useState([]);

  useEffect(() => {
    if (pageCount > 2) {
      const temp = Array.from({ length: pageCount - 2 }, (_, i) => i + 2);
      setPagesInBetween(temp);
    }
  }, [pageCount]);

  useEffect(() => {
    if (pageCount <= 5) {
      setGaps({
        before: false,
        paginationGroup: Array.from({ length: pageCount }, (_, i) => i + 1),
        after: false,
      });
    } else {
      const currentLocation = pagesInBetween.indexOf(page);
      let paginationGroup = [];
      let before = false;
      let after = false;

      if (page === 1) {
        paginationGroup = pagesInBetween.slice(0, 3);
      } else if (page >= pageCount - 2) {
        paginationGroup = pagesInBetween.slice(-3);
      } else {
        paginationGroup = [page - 1, page, page + 1];
      }

      if (paginationGroup[0] > 2) {
        before = true;
      }
      if (paginationGroup[paginationGroup.length - 1] < pageCount - 1) {
        after = true;
      }

      setGaps({ before, paginationGroup, after });
    }
  }, [page, pagesInBetween, pageCount]);

  const changePage = (direction) => {
    setPage((currentPage) => {
      if (direction && currentPage < pageCount) {
        return currentPage + 1;
      } else if (!direction && currentPage > 1) {
        return currentPage - 1;
      }
      return currentPage;
    });
  };

  const setPageSafe = (num) => {
    if (num > pageCount) {
      setPage(pageCount);
    } else if (num < 1) {
      setPage(1);
    } else {
      setPage(num);
    }
  };

  return {
    totalPages: pageCount,
    nextPage: () => changePage(true),
    prevPage: () => changePage(false),
    setPage: setPageSafe,
    firstContentIndex,
    lastContentIndex,
    page,
    gaps,
  };
};
