export function createNavigationItem(
    id,
    title,
    route = null,
    children = []
) {

    return {

        id,

        title,

        route,

        children

    };

}