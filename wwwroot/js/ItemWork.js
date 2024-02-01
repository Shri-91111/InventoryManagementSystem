$(document).ready(function () {
    debugger
    $('#Pid').change(function () {
        debugger
        var mainItemId = $(this).val();
        let suburl = $("#GetSubitem").val();
        if (mainItemId) {
            fetchSubItems(suburl, mainItemId);
        } else {
            resetSubItems();
        }
    });
    $('#amount, #tax').change(function () {
        calculateTotalAmount();
    });

    // Initial calculation
    calculateTotalAmount();

    
});
function calculateTotalAmount() {
    debugger
    var amount = parseFloat($('#amount').val()) || 0;
    var taxText = $('#tax option:selected').text(); // Get the text of the selected option

    // Extract numeric part from the tax text (e.g., "8%", "18%", "28%")
    var tax = parseFloat(taxText.replace('%', '')) || 0;

    var totalAmount = amount + (amount * (tax / 100));

    // Display the total amount in the totalAmount div
    $('#totalAmount').text(totalAmount.toFixed(2));
}
function fetchSubItems(suburl, mainItemId) {
    debugger
    $.ajax({
        type: 'GET',
        url: suburl,
        data: { mainItemId: mainItemId },
        success: function (data) {
            console.log(data);
            try {
                // Check if data is an array
                if (Array.isArray(data)) {
                    updateSubItems(data);
                } else {
                    console.error("Data is not an array:", data);
                    resetSubItems();
                }
            } catch (error) {
                console.error("Error processing data:", error);
                resetSubItems();
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error("Error fetching subitems:", textStatus, errorThrown);
            resetSubItems();
        }
    });
}

function updateSubItems(data) {
    debugger
    $('#Subtypeid').empty().append('<option value="">--Select SubItem--</option>');
    $.each(data, function (i, item) {
        $('#Subtypeid').append('<option value="' + item.value + '">' + item.text + '</option>');
    });
}

function resetSubItems() {
    $('#Subtypeid').empty().append('<option value="">--Select SubItem--</option>');
}
