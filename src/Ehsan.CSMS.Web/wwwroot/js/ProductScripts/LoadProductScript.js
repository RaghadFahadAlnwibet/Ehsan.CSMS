$(function () {
    $('select').each(function () {
        const selectElement = $(this)[0];
        if ($(selectElement).attr('id') === 'SelectedProduct') {
            if (!selectElement.tomselect) {
                new TomSelect(selectElement, {
                    create: true,
                    valueField: 'Id',
                    searchField: 'productName',
                    render: {
                        option: function (data, escape) {
                            return `
                                <div class="ts-option">
                                    <strong class="ts-title">${escape(data.productName)}</strong>
                                    <br>
                                    <span class="ts-category">${escape(data.categoryName)}</span>
                                    <span class="ts-price">${escape(data.price)} SAR</span>
                                </div>
                            `;
                        },
                        item: function (data, escape) {
                            return `
                                <div class="ts-item">
                                    <span class="ts-title">${escape(data.productName)}</span>
                                    &nbsp;|&nbsp;
                                    <span class="ts-category">${escape(data.categoryName)}</span>
                                </div>
                            `;
                        }
                    }
                });
            }
        }
    });
});
