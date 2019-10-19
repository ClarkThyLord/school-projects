#include <iostream>
#include <string>
#include <queue>
#include <osrbtree.hpp>
#include <osrbnode.hpp>
#include <color.h>
#include <iostream>
#include <string>

OSRBTree<int> *setup_tree()
{
    OSRBTree<int> *t{new OSRBTree<int>{10}};
    int values[]{5, 4, 6, 15, 14, 16};
    for (int i = 0; i < 7; i++)
        t->insert(values[i]);
    return t;
}

int main()
{
    OSRBTree<int> *t = setup_tree();

    int res{t->root->left->data};
    auto idk = res == 5;
    std::cout << idk << std::endl;
    delete t;
}
