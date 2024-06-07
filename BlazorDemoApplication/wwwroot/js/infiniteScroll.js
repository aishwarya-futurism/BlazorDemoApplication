window.addInfiniteScroll = (dotnetHelper) => {
    window.onscroll = async () => {
        if (window.innerHeight + window.scrollY >= document.body.offsetHeight) {
            await dotnetHelper.invokeMethodAsync('LoadMoreUsers');
        }
    };
};
